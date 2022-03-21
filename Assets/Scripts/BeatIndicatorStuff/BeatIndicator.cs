using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer mySprite;
    [SerializeField] protected float beatToHit;
    [SerializeField] protected float startBeat;//The beat that this first shows up on
    public bool moving;
    float fadeOutTime = 1f;
    public Vector3 startPos;
    public Vector3 endPos;

    Vector3 distPerBeat;

    void Update()
    {   
        //if(moving) {
            float lerpValue = (BeatController.GetBeat() - startBeat) / (beatToHit - startBeat);
            lerpValue = Mathf.Min(lerpValue, 1);
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if(lerpValue == 1) { //&& !moving) {
                //Deactivates when it reaches 1
                Disable();
            }
        //}
    }

    public virtual void SetIndicatorStart(beatIndicatorInfo info) {
        mySprite.enabled = true;
        beatToHit = info.beatToHit;
        startBeat = beatToHit - BeatIndicatorBrain.beatsInAdvanceShown;
        //Sets position to the starting position
        transform.position = startPos;

        distPerBeat = (endPos - startPos) / (beatToHit - startBeat);
    }

    public void Disable() {
        //moving = false;
        //enabled = false;
        //mySprite.enabled = false;
        StartCoroutine(Disappear());
    }

    public IEnumerator Disappear() {
        Debug.Log("called disappear");
        float beatToStopOn = BeatController.GetBeat() + fadeOutTime;
        while(BeatController.GetBeat() < beatToStopOn) {
            transform.position = endPos + (distPerBeat * (BeatController.GetBeat() - beatToHit));
            yield return null;
        }
        moving = false;
        enabled = false;
        mySprite.enabled = false;
    }

}
