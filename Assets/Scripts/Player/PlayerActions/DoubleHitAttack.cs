using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//An attack action where the player has to successfully hit 2 beats in a row
public class DoubleHitAttack : PlayerAction
{
    //[SerializeField] private float damage;
    [SerializeField] private float secondHitTime = 1f;//The time (approximately) when the second beat should hit
    private bool isAttacking;
    private float secondHitBeat;
    //After the first success, disables other actions for the next beat

    protected override void TryAction()
    {
        //If the player is currently performing this action, it checks that first
        if(Global.Player.CurrentAction == this) {
            //Gets the nearest beat - 
            float hitTimeBeatFraction = secondHitTime % 1;
            if(hitTimeBeatFraction == 0) {
                hitTimeBeatFraction = 1;
            }
            if(BeatController.GetNearestBeat(hitTimeBeatFraction) == secondHitBeat) {
                //if it's not the beat this action started on, 
                    //and the player's accuracy is good enough, the action follows through
                Accuracy curAccuracy = BeatController.GetAccuracy(hitTimeBeatFraction);
                Global.Player.spriteController.DisplayAccuracy(curAccuracy);
            
                if(BeatController.IsOnBeat()) {
                    Hit();
                }
                else {
                    Failure();
                }
            }
            else {
                Failure();
            }

        }
        else {
            base.TryAction();
        }
        
    }

    protected override void Success()
    {
        //After making your first action, it sets it so it's ready to make a second hit
            //Also deals damage on the first attack
        isAttacking = true;
        secondHitBeat = BeatController.GetNearestBeat() + secondHitTime;
        base.Success();
        Global.Boss.ChangeBossHP(-damage);
        myActionIndicator.gameObject.SetActive(true);
        myActionIndicator.PerformAction();
        Global.Player.spriteController.Attack(1);
        
    }

    //This is a secondary Mess-up function for when the player messes up the second action
    private void Failure() {
        //Prevents the player from acting until the end of this Length
        Global.Player.CurrentAction = Global.Player.messUpAction;
        Global.Player.spriteController.MessUp();
        //Debug.Log("MESSUP");
    }

    //This is the secondary success function for when the second attack hit
    private void Hit() {
        Global.Boss.ChangeBossHP(-damage);
        
    }


}
