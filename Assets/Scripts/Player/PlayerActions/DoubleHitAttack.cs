using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//An attack action where the player has to successfully hit 2 beats in a row
public class DoubleHitAttack : PlayerAction
{
    [SerializeField] private float damage;

    //After the first success, disables other actions for the next beat

    

    protected override void Success()
    {
        base.Success();
        Global.Boss.ChangeBossHP(-damage);
        Global.Player.spriteController.Attack(1);
        
    }


}
