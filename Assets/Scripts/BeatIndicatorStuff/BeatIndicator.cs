using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    [SerializeField] public SpriteRenderer mySprite;
    [SerializeField] public float beatToHit {private set; get;}
    [SerializeField] protected float startBeat;//The beat that this first shows up on
    public bool moving;
    float fadeOutTime = 0.25f;
    public Vector3 startPos;
    public Vector3 endPos;

    Vector3 distPerBeat;
    [SerializeField] Color originalColor;

    void Awake() {
        mySprite = GetComponent<SpriteRenderer>();
        originalColor = mySprite.color;
    }

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
        mySprite.color = originalColor;
        mySprite.enabled = true;
        beatToHit = info.beatToHit;
        startBeat = beatToHit - BeatIndicatorBrain.beatsInAdvanceShown;
        //Sets position to the starting position
        transform.position = startPos;

        distPerBeat = (endPos - startPos) / (beatToHit - startBeat);
    }

    public void Disable() {
        StartCoroutine(Disappear());
    }

    public IEnumerator Disappear() {
        float beatToStopOn = BeatController.GetBeat() + fadeOutTime;
        Debug.Log(beatToStopOn + " insideDisappear");
        //Color originalColor = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b);
        while(BeatController.GetBeat() < beatToStopOn) {
            Debug.Log(BeatController.GetBeat());
            transform.position = endPos + (distPerBeat * (BeatController.GetBeat() - beatToHit));

            //fade out
            float fade = Mathf.Lerp(originalColor.a, 0, ((BeatController.GetBeat() - beatToHit)));
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, fade);

            yield return null;
        }
        moving = false;
        mySprite.color = originalColor;
        enabled = false;
        mySprite.enabled = false;
    }

}
