using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSplash : MonoBehaviour
{

    public SpriteRenderer high_OK;
    public SpriteRenderer high_GOOD;
    public SpriteRenderer high_GREAT;
    public SpriteRenderer high_PERFECT;

    public SpriteRenderer low_OK;
    public SpriteRenderer low_GOOD;
    public SpriteRenderer low_GREAT;
    public SpriteRenderer low_PERFECT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSplash(bool wasHigh, BeatController.Accuracy accuracy) {

        //generate a little random position and angle (??? or make each one a little different??)

        //grow 
        //fade out 
        //use coroutines with deltatime probably 

    }

}
