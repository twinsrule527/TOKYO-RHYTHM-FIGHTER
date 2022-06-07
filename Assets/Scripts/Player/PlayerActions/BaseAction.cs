using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A starting action in the game, which works as an attack
public class BaseAction : PlayerAction
{

/*
    protected override void TryAction()
    {
        
        if(Global.Player.CurrentAction == null) {
            Accuracy curAccuracy = BeatController.GetAccuracy(beatFraction);
            //Call PlayerSpriteController.DisplayAccuracy(Accuracy);
            if(curAccuracy.priority > 0) {
                    Success();
            }
            else {
                MessUp();
            }
        }
    }
    */
//Override succcess function if you need to override the function from the original script
    protected override void Success()
    {
        //myActionIndicator.gameObject.SetActive(true);
        //myActionIndicator.PerformAction();
        if(Global.Boss.AttackAI.AttackBeatHitOn != BeatController.GetNearestBeat() || !Global.Boss.AttackAI.CurrentAttackOutgoing.IsAnAttack) {//DOesn't always work correctly
            Global.Player.spriteController.Attack(1);
            Global.CenterEffectManager.CallCenterEffect(CenterEffect.PlayerHits);
            currentActionCoroutine = ActionCoroutine();
            StartCoroutine(currentActionCoroutine);
            //currentActionCoroutine = StartCoroutine(ActionCoroutine());
            base.Success();
        
        }
        else {
            //Play the MessUp/Hurt Animation
            Global.Player.ChangeHP();
        }
    }
    protected override void MessUp()
    {
        base.MessUp();
        Global.CenterEffectManager.CallCenterEffect(CenterEffect.PlayerMisses);
        if (currentActionCoroutine != null) {
            StopCoroutine(currentActionCoroutine);
            Global.Boss.dmgNumber.CancelBossDisplay();
        }
    }
    public override IEnumerator ActionCoroutine()
    {
        //Deals damage at the end of the action, in case it get messed up
        //but, display the potential damage immediately so it feels responsive
        Global.Boss.dmgNumber.ResponsiveDisplayBoss(damage);
        float startTime =  BeatController.GetBeat();
        float t = startTime;
        Global.Boss.ChangeVisualBossHP(-damage);
        Debug.Log("viz " + Global.Boss.bossVisualHP);
        while(t < startTime + length - 0.5f) {
            yield return null;
            t = BeatController.GetBeat();
        }
        Debug.Log("DAMAGED");
        Global.Boss.ChangeBossHP(-damage);
    }
}
