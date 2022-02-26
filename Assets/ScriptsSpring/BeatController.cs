using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{

    public struct Accuracy {
        public Accuracy(float threshBefore, float threshAfter, string nam, int num) {
            this.thresholdBeforeBeat = threshBefore;
            this.thresholdAfterBeat = threshAfter;
            this.name = nam;
            this.number = num;
        }
        public float thresholdBeforeBeat { get; }
        public float thresholdAfterBeat { get; }

        int number;  //for doing faster equals comparisons, i think 
        public string name { get; } //for debugging- TODO remove later?

        //do we need this?
        public override bool Equals(object obj) {
            if (!(obj is Accuracy))
                return false;

            Accuracy other = (Accuracy) obj;
            if(other.number == this.number) 
                return true;
            return false;

        }
    }

    //// Beat accuracies! 
    //You might get these from BeatController functions.
    //You can check them against each other
    //ex. if(accuracyPassedIn.Equals(BeatController.PERFECT)) { do something cause it's perfect }

    //TODO: should there be different thresholds for different fractions of beats? 
    public static readonly Accuracy MINIMUM = new Accuracy(0.30f, 0.25f, "MINIMUM", 0);
    public static readonly Accuracy GREAT = new Accuracy(0.20f, 0.18f, "GREAT", 1);
    public static readonly Accuracy PERFECT = new Accuracy(0.8f, 0.6f, "PERFECT", 10);
    public static readonly Accuracy OFFBEAT = new Accuracy(float.NaN, float.NaN, "OFFBEAT", -1);
    //below: accuracies checked in GetAccuracy() func
    //declared up here so we don't forget anything when making new ones.
    //IN ORDER of checked. So, go SMALLEST TO GREATEST window 
    static Accuracy [] accuraciesToCheck = {PERFECT, GREAT, MINIMUM};

    
    //BPM 
    //easy to know and set. human-readable, will be used to do some conversion 
    static float BPM = 100; 


    public AudioSource audioSource;

    //will be calculated from BPM. in seconds. 
    private static double secPerBeat;

    //time the song started, in unity audio time 
    private static double songStartTime;

    //song position, in seconds 
    //private static float songPos = 0;

    //what beat the song is on ex. 1, 2, 4, 5.5, 6.75 
    //we will use a threshold with this for reaction 
    //public static float beat { get; private set; }
    //^^^^^ REPLCAED WITh GetBeat()

    //how near or far we are from a beat. 
    //1 when exactly on beat (ex. 5.0) 0 when exactly between beats (ex. 5.5)
    //would look like a triangle wave when graphed 
    //public static float beatOffset { get; private set; }
    //^^^ REPLACED WITH GetDistanceFromBeat(1)

    bool beatEnded1, beatEnded05, beatEnded025;


    // Start is called before the first frame update
    void Start()
    {
        //convert BPM to functional value 
        secPerBeat = 60 / BPM;

        //TODO have the song started by a button or soemthing idk. 
        //for now just starts at startup 
        StartSong();

    }

    //call when we start the song. 
    //records the time ect 
    void StartSong() {
        //kick off tracker with current time 
        songStartTime = AudioSettings.dspTime;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(GetBeat() + " " + GetAccuracy(1).name);

        //update all our tracker variables. 
        //songPos = (float)(AudioSettings.dspTime - songStartTime);
        //beat = songPos / secPerBeat;
        //beatOffset = GetAbsDistanceFromBeat(1);
        //^^^^ moved these to more direct getters.


        //If we've hit major beats (1, 0.5, 0.25) send out events.

        float dist = GetDistanceFromBeat(1);
        if(!beatEnded1 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded1 = true;
            Global.Boss.EndOfBeat1();
            Global.Player.EndOfBeat1();
        } else if(dist > 1 - MINIMUM.thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded1 = false;
        }

        dist = GetDistanceFromBeat(0.5f);
        if(!beatEnded05 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded05 = true;
            Global.Boss.EndOfBeat05();
            Global.Player.EndOfBeat05();
        } else if(dist > 0.5 - MINIMUM.thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded05 = false;
        }

        dist = GetDistanceFromBeat(0.25f);
        if(!beatEnded025 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded025 = true;
            Global.Boss.EndOfBeat025(); 
            Global.Player.EndOfBeat025();
        } else if(dist > 0.25 - MINIMUM.thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded025 = false;
        }

    }

    //instead of tracker variables, use more direct getters. 
    public static float GetBeat() {
        double songPos = AudioSettings.dspTime - songStartTime;
        return (float)(songPos / secPerBeat);
    }

    //ex. for 1, 5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.9
    public static float GetDistanceFromBeat(float fraction) {
        return GetBeat() % fraction;
    }

    //ex. for 1, 5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.1
   /* public static float GetAbsDistanceFromBeat() {
        float b = GetDistanceFromBeat() - 0.5f;
        b = -Mathf.Abs(b);
        return b + 0.5f;
    }*/
    public static float GetAbsDistanceFromBeat(float fraction) {
        float b = GetDistanceFromBeat(fraction) - (fraction / 2);
        b = -Mathf.Abs(b);
        return b + (fraction / 2);
    }

    //for use by player actions. 
    //are we on beat, within the actionable threshold, according to a certain fraction?
    public static bool IsOnBeat(float fraction) {
        
        float distFromBeat = GetDistanceFromBeat(fraction);
        float distFromBeatAbs = GetAbsDistanceFromBeat(fraction);

        if(distFromBeat < fraction / 2) {
            //if this is after 
            if(distFromBeat <= MINIMUM.thresholdAfterBeat) {
                return true;
            } else {
                return false;
            }
        } else {
            //if this is before 
            if(distFromBeatAbs <= MINIMUM.thresholdBeforeBeat) {
                return true;
            } else {
                return false;
            }
        }
    }

    //get the current accuracy. returns an Accuracy, which 
    //can be checked against, for example. BeatController.PERFECT 
    public static Accuracy GetAccuracy(float fraction) {
        
        float distFromBeat = GetDistanceFromBeat(fraction);
        float distFromBeatAbs = GetAbsDistanceFromBeat(fraction);

        if(distFromBeat < fraction / 2) {
            //if after
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeat <= a.thresholdAfterBeat) {
                    return a;
                }
            }
        } else {
            //if before 
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeatAbs <= a.thresholdBeforeBeat) {
                    return a;
                }
            }
        }
        return OFFBEAT;
    }

    //Like WaitForSeconds, but in sync with the music. 
    //USE THIS INSTEAD OF WAITFORSECONDS
    //similar code as below, would re-use by calling WaitForBeatsMulti(1) here but it's a coroutine so ? 
    public static IEnumerator WaitForBeat(float fraction) {
        
        float lastDistFromBeat = GetDistanceFromBeat(fraction);
        while(true) {
            float newDistFromBeat = GetDistanceFromBeat(fraction);
            if(lastDistFromBeat > newDistFromBeat) {
                break;
            }
            lastDistFromBeat = newDistFromBeat;
            yield return null;
        }
    }

    //Wait for X number of a type of beat 
    //Ex. wait for 2 0.5s to go by 
    public static IEnumerator WaitForBeatsMulti(int numBeats, float fraction) {
       
        int counter = 0;

        float lastDistFromBeat = GetDistanceFromBeat(fraction);

        while(counter < numBeats) {
            float newDistFromBeat = GetDistanceFromBeat(fraction);
            if(lastDistFromBeat > newDistFromBeat) {
                counter++;
                if(counter >= numBeats) {
                    break;
                }
            }
            lastDistFromBeat = newDistFromBeat;
            yield return null;
        }
    }

    //Wait for the end of the threshold in which you can make an attack. 
    //For use with updating variables after everything has happened on a beat. 
    public static IEnumerator WaitForEndOfThreshold(float fraction) {

        //if we're not on beat, first wait until we're on beat. (we've entered the threshold.)
        if(!IsOnBeat(fraction)) {
            while(!IsOnBeat(fraction)) {
                yield return null;
            }
        }

        //if we're on beat, wait for the end of the threshold.
        while(IsOnBeat(fraction)) {
            yield return null;
        }
    }


}
