using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatIndicator : BeatIndicator
{
    public bool leftIndicator;
    public SpriteRenderer outline;
    Color invisible = new Color(1, 1, 1, 0);
    void Awake()
    {
        startPos = BeatIndicatorBrain.PlayerIndicatorStartPos;
        endPos = BeatIndicatorBrain.PlayerIndicatorEndPos;
        if(mySprite == null) {
            mySprite = GetComponent<SpriteRenderer>();  
        }
    }

    public void SetPlayerIndicatorStart(beatIndicatorInfo info, bool left) {
        leftIndicator = left;
        outline.enabled = leftIndicator;
        showOutline(false);
        SetIndicatorStart(info);
    }
    public override void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.color = originalColor;
        mySprite.enabled = true;
        beatToHit = info.beatToHit;
        startBeat = beatToHit - BeatIndicatorBrain.beatsInAdvanceShown;
        //Sets position to the starting position
        if(leftIndicator) {
            startPos = BeatIndicatorBrain.PlayerIndicatorStartPos;
            endPos = BeatIndicatorBrain.PlayerIndicatorEndPos;
        }
        else {
            startPos = BeatIndicatorBrain.BossIndicatorStartPos;
            endPos = BeatIndicatorBrain.BossIndicatorEndPos;
        }
        transform.position = startPos;
        transform.rotation = startRot;
        distPerBeat = (endPos - startPos) / (beatToHit - startBeat);
    }

    protected override IEnumerator PastCenterCoroutine() {
        yield return base.PastCenterCoroutine();
        showOutline(false);
        //Now, it needs to activate the next available ShowOutline
        Global.BeatIndicatorBrain.ShowNextIndicatorOutline();
    }

    public void showOutline(bool show = true) {
        if(show) {
            outline.color = Color.white;
            mySprite.color = invisible;
        } else {
            outline.color = invisible;
            mySprite.color = originalColor;
        }
    }
    
}
