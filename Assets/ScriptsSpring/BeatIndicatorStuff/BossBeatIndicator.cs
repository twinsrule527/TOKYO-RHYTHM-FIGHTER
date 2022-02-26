using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A beat indicator used for the bossBeat - might end up being consolidated into the general beat indicator
public class BossBeatIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private float beatToHit;
    [SerializeField] private float startBeat;//The beat that this first shows up on
    void Start()
    {
        
    }

    void Update()
    {   
        float lerpValue = (BeatController.GetBeat() - startBeat) / (beatToHit - startBeat);
        lerpValue = Mathf.Min(lerpValue, 1);
        transform.position = Vector3.Lerp(BeatIndicatorBrain.BossIndicatorStartPos, BeatIndicatorBrain.BossIndicatorEndPos, lerpValue);
        if(lerpValue == 1) {
            //Deactivates when it reaches 1
            Disable();
        }
    }

    public void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.enabled = true;
        mySprite.sprite = info.indicatorSprite;
        beatToHit = info.beatToHit;
        startBeat = beatToHit - BeatIndicatorBrain.beatsInAdvanceShown;
        //Sets position to the starting position of boss beats
        transform.position = BeatIndicatorBrain.BossIndicatorStartPos;
    }

    //A function to disable the boss Beat indicator
    public void Disable() {
        mySprite.enabled = false;
        enabled = false;
    }
}
