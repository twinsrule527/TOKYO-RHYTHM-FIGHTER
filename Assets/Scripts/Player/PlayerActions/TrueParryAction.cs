using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a parry action that counts as a block, and then an attack immediaely after
public class TrueParryAction : PlayerAction
{
    [SerializeField] private float damage;
    [SerializeField] private float secondHitTime = 1f;//The time (approximately) when the second beat should hit
    private bool isParrying;
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
        //First action is a block
            //If the enemy is performing a parry-able action, it preps for the parry
        if(Global.Boss.CurrentMakingAttack.Parryable) {
            isParrying = true;
            secondHitBeat = BeatController.GetNearestBeat() + secondHitTime;
        }
        base.Success();
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
