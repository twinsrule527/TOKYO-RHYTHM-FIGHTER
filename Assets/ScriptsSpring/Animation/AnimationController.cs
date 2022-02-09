using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //The only way to streamline is to keep each animation within 6 frames. 
    //This code assumes us to make every animation 6 frames (with just different speeds)


    //Delay is the time played in between frames of animation
    public float delay;
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

        //if (BeatController.IsEarly(beatFraction))
        //{

        //    //if we're early (before the beat hits) 
        //    //delay starting the animation until the beat hits
              //if(beatEnded
        //    //TODO 
        //    //remember, time should be relative to BeatController not to deltaTime. This is because BeatController uses time relative to the music's samples playing, which keeps it in sync with the music at the lowest level, where deltaTime may fall out of sync. 

        //}
        //else if (BeatController.IsLate(beatFraction) {

        //    //if we're late, we might have to skip some frames from the start.  
        //    //calculate how many frames we need to skip, and how much to delay before starting to play the frames. 
        //    //also, we probably want to set the sprite to the first frame we're playing right now using setFrame(#) so the game feels responsive.
        //    //TODO 
        //    //Ditto on using time relative to BeatController rather than deltaTime. 

        //}
        //else
        //{

        //    //miraculously, we're perfectly on beat 
        //    PlayFromFrame(0);
        //}


    }

    //Plays animation from frame and checks if it should loop. 
    private IEnumerator PlayFromFrame(int frame)
    {
        
        WaitForSeconds wait = new WaitForSeconds(delay);
        for (int i = frame; i < Sprites.Count; i++)
        {
            spriteRenderer.sprite = Sprites[i];
            yield return wait;

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
