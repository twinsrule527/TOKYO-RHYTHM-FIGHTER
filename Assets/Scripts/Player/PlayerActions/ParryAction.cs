using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//An action the player can perform which blocks an incoming attack
public class ParryAction : PlayerAction
{
    protected override void Success()
    {
        base.Success();
        //Play the Parry Animation
        Global.Player.spriteController.Parry();
        Global.CenterEffectManager.CallCenterEffect(CenterEffect.Harmonizes);
        //Check to see if the boss is attacking - if they aren't, the combo breaks
        if(BeatController.GetNearestBeat() != Global.Boss.AttackAI.AttackBeatHitOn || Global.Boss.AttackAI.CurrentAttackOutgoing.GetComponent<WaitAttack>() != null){
            Global.ComboIndicator.SetCombo(0);
        }
    }
}
