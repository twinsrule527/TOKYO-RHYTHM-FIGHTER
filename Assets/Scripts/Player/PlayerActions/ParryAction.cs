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
    }
}
