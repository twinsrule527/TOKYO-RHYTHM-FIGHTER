using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Boss AI Superclass - uses a state machine w/ coroutines
    //Manages Boss attack patterns
        //Subclasses are implementations of each boss with actual attack pattern
public abstract class BossAI : MonoBehaviour
{
    protected Bag<AttackPattern> attackBag;
    public BossAttack CurrentAttack { get; private set; }//Whatever the attack the boss is currently on - is needed for Interrupts
    public List<AttackPattern> AttackQueue;

    //[SerializeField] private BossSpriteController mySpriteController; /- For ERIC
    
    public virtual void Start() {
        AttackQueue = new List<AttackPattern>();

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
        for(int i = 0; i < myAttack.coroutines.Count; i++) {
            CurrentAttack = myAttack.coroutines[i];
            //mySpriteController.StartAttackAnim(CurrentAttack.name);//FOR ERIC
            yield return CurrentAttack.StartCoroutine(CurrentAttack.Attack());
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
        for(int i = 0; i < AttackQueue.Count; i++) {
            attackQueueBeatLength += AttackQueue[i].AttackPatternLength();
        }
        //A temp variable in case something breaks
        float temp = 0;
        while(attackQueueBeatLength < BeatIndicatorBrain.beatsInAdvanceShown && temp < 20) {
            temp++;
            AttackPattern newAttack = attackBag.Draw();
            AttackQueue.Add(newAttack);
            attackQueueBeatLength += newAttack.AttackPatternLength();
            //Also, needs to add the attackPattern to the BeatIndicatorBrain
            newAttack.AddAttacksToBeatIndicator();
        }
    }
}
