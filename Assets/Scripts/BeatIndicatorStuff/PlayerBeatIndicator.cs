using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatIndicator : BeatIndicator
{
    private bool leftIndicator;
    void Awake()
    {
        startPos = BeatIndicatorBrain.PlayerIndicatorStartPos;
        endPos = BeatIndicatorBrain.PlayerIndicatorEndPos;
        mySprite = GetComponent<SpriteRenderer>();   
    }

    public void SetPlayerIndicatorStart(beatIndicatorInfo info, bool left) {
        leftIndicator = left;
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
    
}
