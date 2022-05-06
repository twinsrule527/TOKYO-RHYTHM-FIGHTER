using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    [SerializeField] public SpriteRenderer mySprite;
    [SerializeField] public float beatToHit {protected set; get;}
    [SerializeField] protected float startBeat;//The beat that this first shows up on
    public bool moving;
    //float fadeOutTime = BeatController.MINIMUM.thresholdAfterBeat;
    float fadeOutTime = 0;
    public Vector3 startPos;
    public Quaternion startRot;
    public Vector3 endPos;

    protected Vector3 distPerBeat;
    [SerializeField] protected Color originalColor;

    [SerializeField] List<Sprite> animationFrames;
    [SerializeField] protected bool animated = false;

    void Awake() {
        mySprite = GetComponent<SpriteRenderer>();
        originalColor = mySprite.color;
        if(animationFrames.Count > 1) {
            animated = true;
        }
    }
    void Update()
    {   
        //if(moving) {
            float lerpValue = (BeatController.GetBeat() - startBeat) / (beatToHit - startBeat);
            lerpValue = Mathf.Min(lerpValue, 1);
            transform.position = Vector3.Lerp(startPos, endPos, lerpValue);
            if(lerpValue == 1) { //&& !moving) {
                //Deactivates when it reaches 1
                PastCenter();
            }

            if(animated) {
                
                //pick the frame based on where we are between 0 and 1, but dont go over Count-1
                int frame = Mathf.FloorToInt(Mathf.Clamp((lerpValue * animationFrames.Count), 0f, (animationFrames.Count - 1f)));
                mySprite.sprite = animationFrames[frame];
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
        transform.rotation = startRot;
        distPerBeat = (endPos - startPos) / (beatToHit - startBeat);
    }

    //when beat passes the center without being acted on 
    //player default indicators might fade out, boss indicators might look like they're hitting and dealing damage...
    public virtual void PastCenter() {
        StartCoroutine(PastCenterCoroutine());
    }

    protected virtual IEnumerator PastCenterCoroutine() {
        float beatToStopOn = BeatController.GetBeat() + fadeOutTime;
        //Debug.Log(beatToStopOn + " insideDisappear");
        //Color originalColor = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b);
        while(BeatController.GetBeat() < beatToStopOn) {
            //Debug.Log(BeatController.GetBeat());
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

    //make it disappear like it's been hit- like if the player parries this boss indicator
    //TODO use this for the player hitting their own notes, or some other thing tied to the center?
        //might use this to show how on or off beat they are w/ location of effect.
    public void Pop() {
        //TODO
        //stop moving 
        //start a coroutine for whatever visual effect 
        //at the end of the coroutine, disable the indicator 
    }

    protected virtual IEnumerator PopCoroutine() {
        //TODO:
        //stop moving 
        //visual effect
        //disable it at the end 
        yield return null;
        enabled = false;
        mySprite.enabled = false;
    }

    public void SetBeatToHit(float beat) {
        beatToHit = beat;
    }




}
