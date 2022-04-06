using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A starting action in the game, which works as both an attack & a parry
public class BaseAction : PlayerAction
{
    public float damage;//How much damage this attack does

    

    // Update is called once per frame
    void Update()
    {
    }

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
        base.Success();
        //myActionIndicator.gameObject.SetActive(true);
        //myActionIndicator.PerformAction();
        Global.Player.spriteController.Attack(1);
        //if(!Global.Boss.makeAttackThisBeat) {//DOesn't always work correctly
        Global.Player.spriteController.Attack(1);
        Global.Boss.ChangeBossHP(-damage);
        /*}
        else {
            //Play the MessUp/Hurt Animation
        }*/
    }
}
