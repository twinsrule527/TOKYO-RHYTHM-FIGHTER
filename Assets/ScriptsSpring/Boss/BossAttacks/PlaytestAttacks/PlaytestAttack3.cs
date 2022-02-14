using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Sample attack - is interruptable in the second half, but not the first
public class PlaytestAttack3 : BossAttack
{
    public SpriteRenderer mySprite;
    private bool interruptable;
    public override IEnumerator Attack() {
        mySprite.color = Color.blue;
        yield return StartCoroutine(BeatController.WaitForBeat(1));
        interruptable = true;
        mySprite.color = Color.green;
        yield return StartCoroutine(BeatController.WaitForBeat(3));
        mySprite.color = Color.black;
    }

    public override void Interrupt(PlayerAction action) {
        if(interruptable) {
            StopCoroutine("Attack");
            StartCoroutine("Cancel");
        }
    }
    
    public override IEnumerator Cancel()
    {
        mySprite.color = Color.cyan;
        yield return StartCoroutine(BeatController.WaitForBeat(10));
    }

    
    public override void CheckAttackSuccess()
    {
    }
}
