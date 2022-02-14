using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_BossShortAttack : BossAttack
{
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private float damageToDeal;

    public override IEnumerator Attack() {
        mySprite.color = Color.green;
        yield return StartCoroutine(BeatController.WaitForBeat(2));
        //Checks to see if they can hit the player - if they do, the player gets hit
        StartCoroutine(CheckAttackSuccess());
        yield return null;
        mySprite.color = Color.black;
    }   

    public override void Interrupt(PlayerAction action) {

    }

    public override IEnumerator Cancel()
    {
        yield return null;
    }
    
    //Waits until the end of threshold, then checks to see if the attack is successful
    public override IEnumerator CheckAttackSuccess()
    {
        yield return null;
        //CheckPlayerCurrentAction;
        //If player action is on beat, this attack is blocked and does nothing

        //else { Global.PlayerInstance.ChangeHP(-damage);}
    }


}
