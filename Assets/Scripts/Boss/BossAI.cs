using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Boss AI Superclass - uses a state machine w/ coroutines
    //Manages Boss attack patterns
        //Subclasses are implementations of each boss with actual attack pattern
public abstract class BossAI : MonoBehaviour
{
    public List<Bag<AttackPattern>> attackBag {protected set; get;}
    public BossAttack CurrentAttack { get; private set; }//Whatever the attack the boss is currently on - is needed for Interrupts
    public BossAttack CurrentAttackOutgoing;//Whatever attack the boss is making - but doesn't change until after the attack lands, rather than changing at the instance of the beat change
    public float AttackBeatHitOn;//Indicates the beat on which this attack will hit
    public List<AttackPattern> AttackQueue;

    //[SerializeField] private BossSpriteController mySpriteController; /- For ERIC
    
    public virtual void Start() {
        AttackQueue = new List<AttackPattern>();
        CreateAttackPatterns();
    }
    public virtual void SongStarted() {
        
    }
    //Creates attack patterns for the boss - takes it from text files
    public virtual void CreateAttackPatterns() {
        AttackReader atkReader = GetComponent<AttackReader>();
        for(int i = 0; i < atkReader.PatternsText.Count; i++) {
            List<List<char>> newAtks = atkReader.GetPatterns(i);
            AttackCreator.CreateAttackPatterns(newAtks, this, i);
            attackBag[i].Refill();
        }
    }
    //Updates the Boss State after each attack pattern
    public virtual IEnumerator StateUpdate() {
        while(AttackQueue.Count > 0) {
            AttackPattern newAttack = AttackQueue[0];
            AttackQueue.RemoveAt(0);
            RefillAttackQueue();
            yield return StartCoroutine(StartAttacks(newAttack));
        }
    }
    
    public IEnumerator StartAttacks(AttackPattern myAttack) {
        //Debug.Log(myAttack.name);
        //For some reason, all of the coroutines run, but not all have a chance to check for end of Beat
        for(int i = 0; i < myAttack.coroutines.Count; i++) {
            CurrentAttack = myAttack.coroutines[i];
            AttackBeatHitOn = BeatController.GetNearestBeat(1, BeatController.GetBeat() + CurrentAttack.length);
            //mySpriteController.StartAttackAnim(CurrentAttack.name);//FOR ERIC
            yield return StartCoroutine(CurrentAttack.Attack());
            //yield return myAttack.CoroutineSource.StartCoroutine(myAttack.coroutines[i]);
        }
    }
    //When the boss dies, it stops all attacks and does a death coroutine
    void Die() {
        StopAllCoroutines();
        StartCoroutine("Death");
    }

    //Each boss has their own Death Coroutine
    public abstract IEnumerator Death();

    //All bosses have a Coroutine to just wait
    public virtual IEnumerator Wait(float amt = 0) {
        yield return new WaitForSeconds(amt);
    }

    public virtual IEnumerator Reposition(float amt = 0) {
        yield return null;   
    }

    //Adds attack patterns to the attack queue until it has a length longer than the beatIndicatorBrain
    public void RefillAttackQueue() {
        float attackQueueBeatLength = 0;
        //Get the length of the attackQueue
        //Debug.Log(AttackQueue.Count);
        for(int i = 0; i < AttackQueue.Count; i++) {
            attackQueueBeatLength += AttackQueue[i].AttackPatternLength();
        }
        //A temp variable in case something breaks
        float temp = 0;
        while(attackQueueBeatLength < BeatIndicatorBrain.beatsInAdvanceShown && temp < 20) {
            temp++;
            AttackPattern newAttack = attackBag[GameManager.currentStage].Draw();
            AttackQueue.Add(newAttack);
            attackQueueBeatLength += newAttack.AttackPatternLength();
            //Also, needs to add the attackPattern to the BeatIndicatorBrain
            newAttack.AddAttacksToBeatIndicator();
        }
        
    }

    //This is called whenever the boss loses hp, to check whether they move to the next stage
    public virtual void CheckStageChange() {

    }

    public virtual void StartStage(int stageNum) {

    }

}
