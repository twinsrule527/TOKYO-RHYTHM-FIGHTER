using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestAttack1 : BossAttack
{
    public SpriteRenderer mySprite;
    [SerializeField] private float damage;
    private bool canHitPlayer;
    public override IEnumerator Attack() {
        mySprite.color = Color.red;
        yield return StartCoroutine(BeatController.WaitForBeat(3));
        mySprite.color = Color.white;
        //Checks to see if they can hit the player - if they do, the player gets hit
        if(canHitPlayer) {
            Global.Player.ChangeHP(-damage);
        }
        yield return null;
        mySprite.color = Color.black;
    }   

    public override void Interrupt(PlayerAction action) {

    }

    public override IEnumerator Cancel()
    {
        yield return null;
    }

    public override void CheckAttackSuccess()
    {
    }
}
