using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_AnimationController : MonoBehaviour
{
    //The only way to streamline is to keep each animation within 5 frames. 
    //This code assumes us to make every animation 5 frames (with just different speeds)


    //Delay is the time played in between frames of animation. THIS IS THE FLOAT FRACTION
    //delayFraction should be like the beat which each animation frame plays on, like every 0.5 beat (It should hit by frame 3); we can change this later accordingly
    public float delayFraction;
    private float AnimationEnd;
    public bool loops;

    public bool startAnimation;

    public string animationName; 

    public SpriteRenderer spriteRenderer;
    public List<Sprite> Sprites;

    


    private void Awake()
    {
        //for just making the animationController go ahead and begin its own animation
        if (startAnimation) 
        {
            //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            PlayAnimation();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Sprites = new List<Sprite>();
        //Pass renderer here
        //hard drag it in
        //At runtime get component in parent
        //spriteRenderer = gameObject.transform.parent.GetComponentInParent<SpriteRenderer>();

        //gameObject.GetComponentInParent<SpriteRenderer>();
        //gameObject.transform.parent.GetComponentInParent<SpriteRenderer>();


        SetFrame(0);
        AnimationEnd = delayFraction * Sprites.Count;

        


    }

    // Update is called once per frame
    void Update()
    {

    }


    //no sync, plays from 0 frame. 
    public void PlayAnimation()
    {
        StartCoroutine(PlayFromFrame(0));
        SetFrame(0);
    }


    //Whole note for heavy attack, half note attack for medium attack, quarter note attack
    //could start quarter note attack 
    //Change when the attack finishes

    //MAKE THE ATTACKS END on the beat
    public void PlayAnimationOnBeat(float beatFraction)
    {
        //Okay this is how long the attack, where I am, this is how long I will need to play for. 
        if(beatFraction == 0)
        {
            PlayAnimation();
            return;
        }
        //If its early it should be a realm greater than or above the designated half beat
        //Inputting fractions, 

        float distFromBeat = BeatController.GetDistanceFromBeat(beatFraction);
        //modeled after the BeatController checks for early or lateness

        if (distFromBeat >= beatFraction/2)
        {
            //If we are early and off beat
            StartCoroutine(BeatController.WaitForBeat(beatFraction));
            PlayAnimation();

        }
        else if (distFromBeat < beatFraction/2)
        {
            //if we are late
            SetFrame(Sprites.IndexOf(spriteRenderer.sprite));

            //calculate frames to cut out
            int framesToCut = 0;

            //cuts out frames based on how many delayFractions(distance between each frame) would have fit within the abs distance from beat
            framesToCut = (int)(distFromBeat/delayFraction);

            PlayFromFrame(framesToCut);
        }
        else
        {
            PlayAnimation();
        }






    }

    //Plays animation from frame and checks if it should loop. 
    private IEnumerator PlayFromFrame(int frame)
    {

        for (int i = frame; i < Sprites.Count; i++)
        {
            spriteRenderer.sprite = Sprites[i];
            //StartCoroutine(BeatController.WaitForBeat(1.0f));
            yield return StartCoroutine(BeatController.WaitForBeat(delayFraction));
            ;

            if (loops == true)
            {
                if (i == Sprites.Count - 1)
                {
                    i = frame;
                }
            }
        }

        SetFrame(0);
    }

    private void SetFrame(int frame)
    {
        spriteRenderer.sprite = Sprites[frame];
    }
}
