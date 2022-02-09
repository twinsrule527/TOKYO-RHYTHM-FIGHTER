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
        float curTime = BeatController.songPos;
        float endTime = curTime + 2;
        Debug.Log(endTime);
        while(curTime < endTime) {
            yield return null;
            curTime = BeatController.songPos;
        }
        curTime = BeatController.songPos;
        endTime = curTime + 1;
        interruptable = true;
        while(curTime < endTime) {
            yield return null;
            curTime = BeatController.songPos;
        }
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
