using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A beat indicator used for the bossBeat - might end up being consolidated into the general beat indicator
public class BossBeatIndicator : BeatIndicator
{

    //[SerializeField] 
    
    void Awake() {
        startPos = BeatIndicatorBrain.BossIndicatorStartPos;
        endPos = BeatIndicatorBrain.BossIndicatorEndPos;
    }

    public override void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.sprite = info.indicatorSprite;
        base.SetIndicatorStart(info);
    }

    public override void PastCenter() {
        moving = false;
        enabled = false;
        mySprite.enabled = false;
    }
    
}
