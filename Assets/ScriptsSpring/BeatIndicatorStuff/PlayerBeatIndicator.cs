using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatIndicator : MonoBehaviour
{
    [SerializeField] private float beatToHit;
    [SerializeField] private float startBeat;//The beat that this first shows up on
    private SpriteRenderer mySprite;
    void Awake()
    {
     mySprite = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        float lerpValue = (BeatController.GetBeat() - startBeat) / (beatToHit - startBeat);
        lerpValue = Mathf.Min(lerpValue, 1);
        transform.position = Vector3.Lerp(BeatIndicatorBrain.PlayerIndicatorStartPos, BeatIndicatorBrain.PlayerIndicatorEndPos, lerpValue);
        if(lerpValue == 1) { //&& !moving) {
            //Deactivates when it reaches 1
            Disable();
        }
    }

    public void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.enabled = true;
        //mySprite.sprite = info.indicatorSprite;
        beatToHit = info.beatToHit;
        startBeat = beatToHit - BeatIndicatorBrain.beatsInAdvanceShown;
        //Sets position to the starting position of boss beats
        transform.position = BeatIndicatorBrain.PlayerIndicatorStartPos;
    }

    public void Disable() {
        //StartCoroutine(Disappear());
        enabled = false;
        mySprite.enabled = false;
    }
}
