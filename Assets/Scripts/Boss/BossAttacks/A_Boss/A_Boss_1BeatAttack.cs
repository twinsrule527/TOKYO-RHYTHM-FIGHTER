using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Boss_1BeatAttack : BossAttack
{
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private float damageToDeal;

    public override IEnumerator Attack() {
        mySprite.color = Color.white;
        //Call the animation controller
        Global.Boss.spriteController.CallAttack(atkName, 1);
        yield return StartCoroutine(BeatController.WaitForBeat(1));
        //Checks to see if they can hit the player - if they do, the player gets hit
        Global.Boss.makeAttackThisBeat = true;
        Global.Boss.CurrentMakingAttack = this;
        Global.Boss.sfxController.PlayAttackSound(1);
        Global.Player.ChangeVisualHP(-damageToDeal);
        Global.CenterEffectManager.CallCenterEffect(CenterEffect.BossHits);

        //Debug.Log("1");
        //Debug.Log(BeatController.GetBeat());
        yield return null;
        //mySprite.color = Color.black;
    }   

    //This attack cannot be cancelled
    public override void Interrupt(PlayerAction action) {

    }

    public override IEnumerator Cancel()
    {
        yield return null;
    }
    
    //Waits until the end of threshold, then checks to see if the attack is successful
    public override void CheckAttackSuccess()
    {
        if(Global.Player.CurrentAction == null || Global.Player.CurrentAction.GetComponent<ParryAction>() == null) {
            Global.Player.ChangeHP(-damageToDeal);
            Global.Boss.CurrentMakingAttack = null;
        }
        else {
            mySprite.color = Color.white;
            Global.Player.ChangeVisualHP(damageToDeal);
            isParried();

        }
        //CheckPlayerCurrentAction;
        //If player action is on beat, this attack is blocked and does nothing

        //else { Global.PlayerInstance.ChangeHP(-damage);}
    }

    public override void AddBeatToIndicator()
    {
        Global.BeatIndicatorBrain.AddBossBeat(length, attackIndicatorSprite, playsHitAnimation);
    }
}
