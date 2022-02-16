using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A short attack the boss performs over the course of 2 beats
public class A_BossShortAttack : BossAttack
{
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private float damageToDeal;

    public override IEnumerator Attack() {
        mySprite.color = Color.green;
        yield return StartCoroutine(BeatController.WaitForBeat(1));
        mySprite.color = Color.red;
        yield return StartCoroutine(BeatController.WaitForBeat(1f));
        mySprite.color = Color.white;
        //Checks to see if they can hit the player - if they do, the player gets hit
        Global.Boss.makeAttackThisBeat = true;
        Global.Boss.CurrentMakingAttack = this;
        yield return null;
        mySprite.color = Color.black;
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

        if(Global.Player.CurrentAction == null || Global.Player.CurrentAction == Global.Player.messUpAction) {
            Global.Player.ChangeHP(-damageToDeal);
        }
        else {
            mySprite.color = Color.white;
        }
        //CheckPlayerCurrentAction;
        //If player action is on beat, this attack is blocked and does nothing

        //else { Global.PlayerInstance.ChangeHP(-damage);}
    }


}
