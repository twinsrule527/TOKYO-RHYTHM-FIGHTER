using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{

    /*public enum Accuracy {
        OK, GOOD, GREAT, PERFECT, OFFBEAT
    }*/
    public AudioSource audioSource;
    public static BeatController Instance;
    //BPM 
    //easy to know and set. human-readable, will be used to do some conversion 
    static float BPM = 100; 

    //will be calculated from BPM. in seconds. 
    static float secPerBeat;

    //song position, in seconds 
    static float songPos = 0;

    //what beat the song is on ex. 1, 2, 4, 5.5, 6.75 
    //we will use a threshold with this for reaction 
    public static float beat { get; private set; }

    //time the song started, in unity audio time 
    static float songStartTime;
    
    //how near or far we are from a beat. 
    //1 when exactly on beat (ex. 5.0) 0 when exactly between beats (ex. 5.5)
    //would look like a triangle wave when graphed 
    public static float beatOffset { get; private set; }

    //public static Accuracy accuracy;


    //thresholds to be on beat, in seconds. 
    //OK represents the overall threshold. 
    /*
    public static float thresh_OK = 0.20f;
    public static float thresh_GOOD = 0.15f;
    public static float thresh_GREAT = 0.10f;
    public static float thresh_PERFECT = 0.05f;
    */


    //TODO: should there be different thresholds for different fractions of beats? 

    [SerializeField] static float thresholdBeforeBeat;
    [SerializeField] static float thresholdAfterBeat;

    bool beatEnded1, beatEnded05, beatEnded025;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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
        //accuracy = GetAccuracy();

        //If we've hit major beats (1, 0.5, 0.25) send out events. 
        if(!beatEnded1 && getDistanceFromBeat(1) > thresholdAfterBeat) {
            beatEnded1 = true;
            //TODO send out events 
        } else if(getDistanceFromBeat(1) > 1 - thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded1 = false;
        }

        if(!beatEnded05 && getDistanceFromBeat(0.5f) > thresholdAfterBeat) {
            beatEnded05 = true;
            //TODO send out events 
        } else if(getDistanceFromBeat(0.5f) > 0.5 - thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded05 = false;
        }

        if(!beatEnded1 && getDistanceFromBeat(0.25f) > thresholdAfterBeat) {
            beatEnded025 = true;
            //TODO send out events 
        } else if(getDistanceFromBeat(0.25f) > 0.25 - thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded025 = false;
        }
        
    }


    //5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.9
    static float getDistanceFromBeat() {
        return beat % 1;
    }

    static float getDistanceFromBeat(float fraction) {
        return beat % fraction;
    }

    //5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.1
    public static float getAbsDistanceFromBeat() {
        float b = getDistanceFromBeat() - 0.5f;
        b = -Mathf.Abs(b);
        return b + 0.5f;
    }
    
    public static float getAbsDistanceFromBeat(float fraction) {
        float b = getDistanceFromBeat(fraction) - (fraction / 2);
        b = Mathf.Abs(b);
        return b + (fraction / 2);
    }

    //for use by player actions. 
    //are we on beat, within the threshold, according to a certain fraction?
    public bool IsOnBeat(float fraction) {
        
        float distFromBeat = getDistanceFromBeat(fraction);

        if(distFromBeat < fraction / 2) {
            //if this is after 
            if(distFromBeat <= thresholdAfterBeat) {
                return true;
            } else {
                return false;
            }
        } else {
            //if this is before 
            if(getAbsDistanceFromBeat(fraction) <= thresholdBeforeBeat) {
                return true;
            } else {
                return false;
            }
        }
    }

    //Like WaitForSeconds, but in sync with the music. 
    //USE THIS INSTEAD OF WAITFORSECONDS
    public static IEnumerator WaitForBeat(float fraction) {
        float lastDistFromBeat = getDistanceFromBeat(fraction);
        while(lastDistFromBeat <= getDistanceFromBeat(fraction)) {
            lastDistFromBeat = getDistanceFromBeat(fraction);
            yield return null;
        }
    }


    //get the current accuracy. returns OK, GOOD, GREAT, PERFECT according to thresholds. 
    /*
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
        
    } */ 
    


}
