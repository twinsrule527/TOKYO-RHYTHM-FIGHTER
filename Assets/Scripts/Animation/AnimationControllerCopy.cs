using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerCopy : MonoBehaviour
{
    
    [SerializeField] private float animDuration = 0.75f;//How long the animation lasts (total time)    
    [SerializeField] private Vector3 lerpDistance;//the distance the animation bounces the object from their base pos
    [SerializeField] private float initialLerpTime = 0.25f;
    [SerializeField] private float snapbackLerpTime = 0;
    [SerializeField] private AnimationControllerCopy AnimSwitchTo;//The animaton the object switches to after this one
    [SerializeField] private Sprite spriteSwitchTo;//If the animation just switches to a single sprite
    [SerializeField] private Sprite AnimSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform spriteTransform;

    [SerializeField] private bool playOnAwake;

    


    private void Awake()
    {
        spriteTransform = spriteRenderer.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        

        


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) {
            StartCoroutine(PlayAnimation());
        }
    }


    public IEnumerator PlayAnimation() {
        //Gets the sprite's position, as well as the current beat
        float animStartTime = BeatController.GetBeat();
        Vector3 animStartPos = spriteTransform.position;
        //Switches the sprite's sprite
        spriteRenderer.sprite = AnimSprite;
        float curTime = BeatController.GetBeat();
        while(curTime < animStartTime + initialLerpTime) {
            spriteTransform.position = Vector3.Lerp(animStartPos, animStartPos + lerpDistance, (curTime - animStartTime) / initialLerpTime);
            curTime = BeatController.GetBeat();
            yield return null;
        }
        spriteTransform.position = animStartPos + lerpDistance;
        //Get when the animation should pop back
        float lerpBackStartTime = animStartTime + animDuration - snapbackLerpTime;
        //Animation waits until it pops back
        yield return StartCoroutine(BeatController.WaitForBeat(lerpBackStartTime));
        curTime = BeatController.GetBeat();
        while(curTime < animStartTime + animDuration) {
            spriteTransform.position = Vector3.Lerp(animStartPos + lerpDistance, animStartPos, (curTime - lerpBackStartTime) / snapbackLerpTime);
            curTime = BeatController.GetBeat();
            yield return null;
        }
        spriteTransform.position = animStartPos;
        if(AnimSwitchTo != null) {
            StartCoroutine(AnimSwitchTo.PlayAnimation());
        }
        else if(spriteSwitchTo != null) {
            spriteRenderer.sprite = spriteSwitchTo;
        }
    }
}
