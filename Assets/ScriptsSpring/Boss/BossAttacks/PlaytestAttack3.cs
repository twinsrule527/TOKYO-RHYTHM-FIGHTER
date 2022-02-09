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
        yield return BeatController.Instance.StartCoroutine(BeatController.WaitForBeat(1));
        interruptable = true;
        yield return BeatController.Instance.StartCoroutine(BeatController.WaitForBeat(1));
        mySprite.color = Color.black;
    }

    public override void Interrupt() {
        if(interruptable) {
            StopCoroutine("Attack");
            StartCoroutine("Cancel");
        }
    }
    
    public override IEnumerator Cancel()
    {
        yield return null;
    }
}
