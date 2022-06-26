using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A boss attack that waits until a given beat/waits a certain amt
public class WaitAttack : BossAttack
{
    [SerializeField] private int beatToWait;
    public override IEnumerator Attack() {
        yield return BeatController.WaitForBeatsMulti(beatToWait, 1);
        Global.Boss.CurrentMakingAttack = this;
        //Debug.Log("Wait");
        yield return null;
    }

    public override void Interrupt(PlayerAction action) {

    }
    
    public override IEnumerator Cancel()
    {
        yield return null;
    }

    
    public override void CheckAttackSuccess()
    {
    }
    public override void AddBeatToIndicator()
    {
        Global.BeatIndicatorBrain.AddBossBeat(beatToWait, attackIndicatorSprite, playsHitAnimation);
    }
}
