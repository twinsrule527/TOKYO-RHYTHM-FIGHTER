using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : SpriteController
{
    //public SpriteRenderer trueRenderer;

    [SerializeField] AnimationController attack;
    [SerializeField] AnimationController messUp;
    [SerializeField] AnimationController hurt;
    [SerializeField] AnimationController idle;
    [SerializeField] AnimationController parry;




    [SerializeField] AccuracyPrefab accuracyTOO_EARLY, accuracyTOO_LATE, accuracyMINIMUM, accuracyGREAT, accuracyPERFECT, accuracyMESSUP;



    private void Awake()
    {
        SpriteRenderer Srenderer = GetComponentInParent<SpriteRenderer>();
        basePosition = Srenderer.transform.position;
        attack.spriteRenderer = Srenderer;
        messUp.spriteRenderer = Srenderer;
        hurt.spriteRenderer = Srenderer;
        idle.spriteRenderer = Srenderer;
        parry.spriteRenderer = Srenderer;


    }

    public void SongStarted() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //trueRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    //Attack is usually played as animation on beat
    public void Attack(float beatFraction)
    {
        //attack.spriteRenderer.sprite = attack.Sprites[0];
        attack.PlayAnimation();
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void MessUp()
    {
        //messUp.spriteRenderer.sprite = messUp.Sprites[0];
        messUp.PlayAnimation();
    }

    //Getting hurt animation should also be played immediately. 
    //Might replace with a block/Parry
    public void Hurt()
    {
        //hurt.spriteRenderer.sprite = hurt.Sprites[0];
        hurt.PlayAnimation();
    }

    public void Idle()
    {
        //idle.spriteRenderer.sprite = idle.Sprites[0];
        idle.PlayAnimation();
    }

    public void Parry()
    {
        //parry.spriteRenderer.sprite = parry.Sprites[0];
        parry.PlayAnimation();
    }

    public void DisplayAccuracy(Accuracy acc) {

        //only show one at a time
        StopDisplayingAccuracies();

        if(acc.Equals(BeatController.TOO_EARLY)) {
            accuracyTOO_EARLY.DisplayAccuracy();
        } else if(acc.Equals(BeatController.TOO_LATE)) {
            accuracyTOO_LATE.DisplayAccuracy();
        } else if(acc.Equals(BeatController.MINIMUM)) {
            accuracyMINIMUM.DisplayAccuracy();
        } else if(acc.Equals(BeatController.GREAT)) {
            accuracyGREAT.DisplayAccuracy();
        } else if(acc.Equals(BeatController.PERFECT)) {
            accuracyPERFECT.DisplayAccuracy();
        } else {
            Debug.Log("ERROR: an Accuracy was passed to PlayerSpriteController.DisplayAccuracy that doesn't match any of the ones checked!!!");
        }

    }
    //If all accuracies need to be ended prematurely
    public void StopDisplayingAccuracies() {
        accuracyTOO_EARLY.StopDisplay();
        accuracyTOO_LATE.StopDisplay();
        accuracyMINIMUM.StopDisplay();
        accuracyGREAT.StopDisplay();
        accuracyPERFECT.StopDisplay();
        accuracyMESSUP.StopDisplay();
    }

    //This is separate from DisplayAccuracy bc its not triggered in the same way
        //Is separate from SpriteController.MessUp bc there are times when the player messes up but this isn't shown
    public void DisplayMessup() {
        StopDisplayingAccuracies();
        accuracyMESSUP.DisplayAccuracy();
    }

}

