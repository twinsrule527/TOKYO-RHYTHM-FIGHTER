using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A beat indicator used for the bossBeat - might end up being consolidated into the general beat indicator
public class BossBeatIndicator : BeatIndicator
{

    BossHitsPlayerScript bossHitsPlayerEffect;
    public bool doesBossHitsPlayerEffect = true;
    private Animator myAnimator;

    void Awake() {
        startPos = BeatIndicatorBrain.BossIndicatorStartPos;
        endPos = BeatIndicatorBrain.BossIndicatorEndPos;
        bossHitsPlayerEffect = BeatIndicatorBrain.BossHitsPlayerEffect;
        myAnimator = GetComponent<Animator>();
    }

    public override void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.sprite = info.indicatorSprite;
        doesBossHitsPlayerEffect = info.playsAnimationAtEnd;
        if(mySprite.sprite == null) {
            animated = false;
        }
        else {
            animated = true;
        }
        base.SetIndicatorStart(info);
    }

    public override void PastCenter() {
        if(doesBossHitsPlayerEffect) {
            bossHitsPlayerEffect.enabled = true;
        }
        moving = false;
        //TODO wait a slight amount of time before disappearing. might override PastCenterCoroutine instead
        enabled = false;
        mySprite.enabled = false;
        if(doesBossHitsPlayerEffect) {
            bossHitsPlayerEffect.enabled = false;
        }
        
    }
    
}
