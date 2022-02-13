using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //The only way to streamline is to keep each animation within 6 frames. 
    //This code assumes us to make every animation 6 frames (with just different speeds)


    //Delay is the time played in between frames of animation. THIS IS THE FLOAT FRACTION
    public float delayFraction;
    public float AnimationEnd;
    public bool loops;

    public SpriteRenderer spriteRenderer;
    public List<Sprite> Sprites;

    public Sprite frame0;
    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;
    public Sprite frame5;


    // Start is called before the first frame update
    void Start()
    {
        Sprites = new List<Sprite>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Sprites.Add(frame0);
        Sprites.Add(frame1);
        Sprites.Add(frame2);
        Sprites.Add(frame3);
        Sprites.Add(frame4);
        Sprites.Add(frame5);

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
        PlayFromFrame(0);
    }

    public void PlayAnimationOnBeat(float beatFraction)
    {
        //if (BeatController.getAbsDistanceFromBeat(beatFraction) > BeatController.thresholdBeforeBeat)
        //{
        //    //If we are early and off beat
        //    StartCoroutine(BeatController.WaitForBeat(beatFraction));
        //    PlayAnimation();

        //} else if ((BeatController.getAbsDistanceFromBeat(beatFraction) > BeatController.thresholdAfterBeat))
        //{
        //    //if we are late
        //    SetFrame(Sprites.IndexOf(spriteRenderer.sprite));

        //    //calculate frames to cut out
        //    int framesToCut = 0 ;

        //    //cuts out frames based on how many delayFractions(distance between each frame) would have fit within the abs distance from beat
        //    framesToCut = (int) (BeatController.getAbsDistanceFromBeat(beatFraction) % delayFraction);
            
        //    PlayFromFrame(framesToCut);
        //} else
        //{
        //    PlayAnimation();
        //}



      


    }

    //Plays animation from frame and checks if it should loop. 
    private IEnumerator PlayFromFrame(int frame)
    {

        for (int i = frame; i < Sprites.Count; i++)
        {
            spriteRenderer.sprite = Sprites[i];
            StartCoroutine(BeatController.WaitForBeat(delayFraction));
            yield return null;

            if (loops == true)
            {
                if (i == Sprites.Count - 1)
                {
                    i = frame;
                }
            }
        }
    }


    private void SetFrame(int frame)
    {
        spriteRenderer.sprite = Sprites[frame];
    }
}
