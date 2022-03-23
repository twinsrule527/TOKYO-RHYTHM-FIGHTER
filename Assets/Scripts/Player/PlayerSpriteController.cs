using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    //public SpriteRenderer trueRenderer;

    [SerializeField] AnimationController attack;
    [SerializeField] AnimationController messUp;
    [SerializeField] AnimationController hurt;
    [SerializeField] AnimationController idle;
    [SerializeField] AnimationController parry;




    [SerializeField] AccuracyPrefab accuracyTOO_EARLY, accuracyTOO_LATE, accuracyMINIMUM, accuracyGREAT, accuracyPERFECT;



    private void Awake()
    {
        attack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        messUp.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        hurt.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        idle.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        parry.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();


    }

    // Start is called before the first frame update
    void Start()
    {
        
        //trueRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.O))
        //{
        //    Attack(1.0f);
        //}

        //if (Input.GetKey(KeyCode.P))
        //{
        //    Hurt();
        //}
    }

    //Attack is usually played as animation on beat
    public void Attack(float beatFraction)
    {
        attack.spriteRenderer.sprite = attack.Sprites[0];
        attack.PlayAnimationOnBeat(beatFraction);
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void MessUp()
    {
        messUp.spriteRenderer.sprite = messUp.Sprites[0];
        messUp.PlayAnimation();
    }

    //Getting hurt animation should also be played immediately. 
    //Might replace with a block/Parry
    public void Hurt()
    {
        hurt.spriteRenderer.sprite = hurt.Sprites[0];
        hurt.PlayAnimation();
    }

    public void Idle()
    {
        idle.spriteRenderer.sprite = idle.Sprites[0];
        idle.PlayAnimation();
    }

    public void Parry()
    {
        parry.spriteRenderer.sprite = parry.Sprites[0];
        parry.PlayAnimation();
    }

    public void DisplayAccuracy(Accuracy acc) {

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
}
