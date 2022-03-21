using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatIndicator : BeatIndicator
{
    void Awake()
    {
        startPos = BeatIndicatorBrain.PlayerIndicatorStartPos;
        endPos = BeatIndicatorBrain.PlayerIndicatorEndPos;
        mySprite = GetComponent<SpriteRenderer>();   
    }
    
}
