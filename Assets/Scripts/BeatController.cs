using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{

    public enum Accuracy {
        OK, GOOD, GREAT, PERFECT 
    }

    public Character ch1;
    public Character ch2;

    //BPM 
    //easy to know and set. human-readable, will be used to do some conversion 
    public static float BPM = 110; 

    //song position, in seconds 
    public static float songPos = 0;

    //when the song started, in unity audio time 
    public static float songStartTime;

    //what beat the song is on ex. 1, 2, 4, 5.5, 6.75 
    //we will use a threshold with this for reaction 
    public static float beat = 0;

    //will be calculated from BPM. in seconds. 
    public static float secPerBeat;

    //how near or far we are from a beat. 
    //1 when exactly on beat (ex. 5.0) 0 when exactly between beats (ex. 5.5)
    //would look like a triangle wave when graphed 
    public static float beatOffset = 0;


    //thresholds to be on beat, in seconds. 
    //OK represents the overall threshold. 
    public static float thresh_OK = 0.1f;
    public static float thresh_GOOD = 0.08f;
    public static float thresh_GREAT = 0.05f;
    public static float thresh_PERFECT = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        //TODO convert BPM to functional value 



    }

    // Update is called once per frame
    void Update()
    {
        //update all our tracker variables. 
        //TODO 

        //check if we've passed the end of the threshold of this beat. 
        //if we have, call the end of beat functions for both players. 
        if(getDistanceFromBeat() > thresh_OK) {
            ch1.endOfBeat();
            ch2.endOfBeat();
        }
        
    }

    //check if an action is on beat. 
    public static bool IsOnBeat() {
        return (getAbsDistanceFromBeat() < thresh_OK);
    }

    //get the current accuracy. returns OK, GOOD, GREAT, PERFECT according to thresholds. 
    public static Accuracy GetAccuracy() {
        float dist = getAbsDistanceFromBeat();
        return Accuracy.GOOD;
    }


    //5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.9
    static float getDistanceFromBeat() {
        return beat % 1;
    }

    //5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.1
    public static float getAbsDistanceFromBeat() {
        float b = getDistanceFromBeat() - 0.5f;
        b = -Mathf.Abs(b);
        return b + 0.5f;
    }

    


}
