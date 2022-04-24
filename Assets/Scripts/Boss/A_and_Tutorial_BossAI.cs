using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This BossAI is the active AI while the tutorial is occuring + the main boss
public class A_and_Tutorial_BossAI : BossAI
{

    [SerializeField] private float bossBeat;//The beat that this goes along with
    [SerializeField] private SpriteRenderer mySprite;//This will eventually go in a separate object
    [SerializeField] private List<float> stageChangeHP;
    public override void Start() {
        attackBag = new Bag<AttackPattern>();
        base.Start();
        attackBag.Refill();
        
    }
    //Called by Boss SongStarted
    public override void SongStarted()
    {
        base.SongStarted();
        RefillAttackQueue();
        StartCoroutine("StateUpdate");
    }

    public override IEnumerator Death() {
        yield return null;
    }

    public override void CheckStageChange()
    {
        //If it's in a position to go to the next stage, it does
        if(Global.Boss.bossHP < stageChangeHP[GameManager.currentStage]) {
            //During the tutorial, the boss goes to the next stage after a button is pressed
            if(Global.Tutorial) {
                Global.TutorialManager.SetUpNextStage();
            }
            //Normally, the boss will automatically go to the next stage
            else {

            }
        }
    }

    private void StartStage(int stageNum) {

    }
}
