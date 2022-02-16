using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_BeatController : MonoBehaviour
{

    public enum Accuracy {
        OK, GOOD, GREAT, PERFECT, OFFBEAT
    }

    public AudioSource audioSource;

    public Character ch1;
    public Character ch2;

    //BPM 
    //easy to know and set. human-readable, will be used to do some conversion 
    public static float BPM = 100; 

    //will be calculated from BPM. in seconds. 
    public static float secPerBeat;

    //song position, in seconds 
    public static float songPos = 0;

    //what beat the song is on ex. 1, 2, 4, 5.5, 6.75 
    //we will use a threshold with this for reaction 
    public static float beat = 0;

    //time the song started, in unity audio time 
    public static float songStartTime;
    
    //how near or far we are from a beat. 
    //1 when exactly on beat (ex. 5.0) 0 when exactly between beats (ex. 5.5)
    //would look like a triangle wave when graphed 
    public static float beatOffset = 0;

    public static Accuracy accuracy;


    //thresholds to be on beat, in seconds. 
    //OK represents the overall threshold. 
    public static float thresh_OK = 0.20f;
    public static float thresh_GOOD = 0.15f;
    public static float thresh_GREAT = 0.10f;
    public static float thresh_PERFECT = 0.05f;

    bool beatEnded = false;


    // Start is called before the first frame update
    void Start()
    {
        //convert BPM to functional value 
        secPerBeat = 60f / BPM;


        //TODO might have the song started by a button or soemthing idk. 
        //for now just starts at startup 
        startSong();


    }

    //call when we start the song. 
    //records the time ect 
    void startSong() {
        //kick off tracker with current time 
        songStartTime = (float)AudioSettings.dspTime;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //update all our tracker variables. 
        songPos = (float)(AudioSettings.dspTime - songStartTime);
        beat = songPos / secPerBeat;
        beatOffset = getAbsDistanceFromBeat();
        accuracy = GetAccuracy();

        //check if we've passed the end of the threshold of this beat. 
        //if we have, call the end of beat functions for both players. 
        if(!beatEnded && getDistanceFromBeat() > thresh_GOOD) {
            beatEnded = true;
            ch1.endOfBeat();
            ch2.endOfBeat();
        } else if(getDistanceFromBeat() > 0.6f) {   //if we've moved on to the next beat, open this flag 
            beatEnded = false;
        }
        
    }

    //check if an action is on beat. 
    public static bool IsOnBeat() {
        return (getAbsDistanceFromBeat() < thresh_OK);
    }

    //get the current accuracy. returns OK, GOOD, GREAT, PERFECT according to thresholds. 
    public static Accuracy GetAccuracy() {

        float dist = getAbsDistanceFromBeat();
        if(dist < thresh_PERFECT) {
            return Accuracy.PERFECT;
        } else if(dist < thresh_GREAT) {
            return Accuracy.GREAT;
        } else if(dist < thresh_GOOD) {
            return Accuracy.GOOD;
        } else if(dist < thresh_OK) {
            return Accuracy.OK;
        } else {
            return Accuracy.OFFBEAT;
        }
        
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
