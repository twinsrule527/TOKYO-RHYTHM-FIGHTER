using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    [SerializeField] private float animDuration = 0.75f;//How long the animation lasts (total time)    
    [SerializeField] private Vector3 lerpDistance;//the distance the animation bounces the object from their base pos
    [SerializeField] private float initialLerpTime = 0.25f;
    [SerializeField] private float initialLerpCoefficient = 0.05f;
    [SerializeField] private float snapbackLerpTime = 0;
    [SerializeField] private float snapbackLerpCoefficient = 0;
    [SerializeField] private AnimationCurve lerpCurve;
    [SerializeField] private AnimationController AnimSwitchTo;//The animaton the object switches to after this one
    [SerializeField] private Sprite spriteSwitchTo;//If the animation just switches to a single sprite
    [SerializeField] private Sprite AnimSprite;

    public SpriteRenderer spriteRenderer;
    private Transform spriteTransform;
    private SpriteController spriteParent;//The parent to this spritecontroller
    [SerializeField] private bool playOnAwake;

    


    private void Awake()
    {
        spriteParent = GetComponentInParent<SpriteController>();
        spriteTransform = spriteRenderer.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        

        


    }

    void Update()
    {
    }

    public void PlayAnimation() {
        if(spriteParent.currentCoroutine != null) {
            StopCoroutine(spriteParent.currentCoroutine);
        }
        spriteTransform.position = spriteParent.basePosition;
        spriteParent.currentCoroutine = RunAnimation();
        StartCoroutine(spriteParent.currentCoroutine);
    }
    public IEnumerator RunAnimation() {
        //Gets the sprite's position, as well as the current beat
        float animStartTime = BeatController.GetBeat();
        Vector3 animStartPos = spriteTransform.position;
        //Switches the sprite's sprite
        spriteRenderer.sprite = AnimSprite;
        float curTime = 0;
        while(curTime < initialLerpTime) {
            spriteTransform.position = Vector3.Lerp(animStartPos, animStartPos + lerpDistance, lerpCurve.Evaluate(curTime / initialLerpTime));
            curTime = BeatController.GetBeat() - animStartTime;
            yield return null;
        }
        spriteTransform.position = animStartPos + lerpDistance;
        //Get when the animation should pop back
        float lerpBackStartTime = animStartTime + animDuration - snapbackLerpTime;
        //Animation waits until it pops back
        yield return StartCoroutine(BeatController.WaitForBeat(lerpBackStartTime));
        curTime = BeatController.GetBeat();
        while(curTime < animStartTime + animDuration) {
            spriteTransform.position = Vector3.Lerp(spriteTransform.position, animStartPos, snapbackLerpCoefficient);
            curTime = BeatController.GetBeat();
            yield return null;
        }
        spriteTransform.position = animStartPos;
        if(AnimSwitchTo != null) {
            AnimSwitchTo.PlayAnimation();
        }
        else if(spriteSwitchTo != null) {
            spriteRenderer.sprite = spriteSwitchTo;
        }
    }
}
