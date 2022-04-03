using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Accuracy {
    public Accuracy(float threshBefore, float threshAfter, string nam, int num) {
        this.thresholdBeforeBeat = threshBefore;
        this.thresholdAfterBeat = threshAfter;
        this.name = nam;
        this.priority = num;
    }
    public float thresholdBeforeBeat { get; }
    public float thresholdAfterBeat { get; }

    //for doing faster equals comparisons, i think 
    //Negative = Off-beat, positive = on-beat
    public int priority;  
    public string name { get; } //for debugging- TODO remove later?

    //do we need this?
    public override bool Equals(object obj) {
        if (!(obj is Accuracy))
            return false;

        Accuracy other = (Accuracy) obj;
        if(other.priority == this.priority) 
            return true;
        return false;
    }
}

public class BeatController : MonoBehaviour
{

    //// Beat accuracies! 
    //You might get these from BeatController functions.
    //You can check them against each other
    //ex. if(accuracyPassedIn.Equals(BeatController.PERFECT)) { do something cause it's perfect }

    //TODO: should there be different thresholds for different fractions of beats? 
    //they could be repurposed as percents?
    public static readonly Accuracy MINIMUM = new Accuracy(0.12f, 0.12f, "OK", 1);
    public static readonly Accuracy GREAT = new Accuracy(0.08f, 0.08f, "GREAT", 5);
    public static readonly Accuracy PERFECT = new Accuracy(0.04f, 0.04f, "PERFECT", 9);
    public static readonly Accuracy TOO_EARLY = new Accuracy(float.NaN, float.NaN, "TOO EARLY", -1);
    public static readonly Accuracy TOO_LATE = new Accuracy(float.NaN, float.NaN, "TOO LATE", -2);
    //below: accuracies checked in GetAccuracy() func
    //declared up here so we don't forget anything when making new ones.
    //IN ORDER of checked. So, go SMALLEST TO GREATEST window 
    static Accuracy [] accuraciesToCheck = {PERFECT, GREAT, MINIMUM};


    //BPM 
    //easy to know and set. human-readable, will be used to do some conversion 
    public static double BPM { get; private set; }

    [SerializeField] AudioSource _audioSource;
    public static AudioSource audioSource; //TODO temp weird thing lol 
    [SerializeField] GameObject songDataHolder;
    List<SongData> songDataList = new List<SongData>();
    [SerializeField] SongData _songToPlay;
    public static SongData songToPlay; //TODO another temp weird thing 

    //will be calculated from BPM. in seconds. 
    private static double secPerBeat;

    //time the song started, in unity audio time 
    private static double songStartTime;

    //song position, in seconds 
    private static double audioPos = 0;
    private static double timePos = 0;

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

        audioSource = _audioSource;
        songToPlay = _songToPlay;

        //populate the list of songs with refs to their data 
        songDataHolder.GetComponentsInChildren<SongData>(songDataList);

        if(songDataList.Count <= 0) {
            Debug.Log("ERROR: no song data found by the BeatController! (Is an object with SongData components provided?)");
        }

    }

    //start the song with the given name. 
    public void StartSongByName(string songName) {
        foreach(SongData data in songDataList) {
            if(data.songName.Equals(songName)) {
                StartSong(data);
                return;
            }
        }
        Debug.Log("ERROR: StartSongByName tried to play song \"" + songName + ",\" which was not found in the song data list!");
    }

    //uses the song given to the beatcontroller object.
    public static void StartSong() {
        StartSong(songToPlay);
    }

    //call when we start the song. 
    //records the time, sets BPM/info, ect 
    public static void StartSong(SongData songData) {
    
        //set up BPM 
        BPM = songData.BPM;
        secPerBeat = 60 / BPM;

        //set the audio source audio clip to this song 
        audioSource.clip = songData.songAudioClip;

        //set a start time just in case other stuff needs it 
        songStartTime = AudioSettings.dspTime + songData.mp3Delay + songData.songDelay;
        //set off all stuff related to the song playing- lag spike here 
        GameManager.SongStarted(songData);

        audioSource.Play();
        //kick off tracker with current time after lag spike
        songStartTime = AudioSettings.dspTime + songData.mp3Delay + songData.songDelay;

    }

    // Update is called once per frame
    void Update()
    {
        //update all our tracker variables. 
        //songPos = (float)(AudioSettings.dspTime - songStartTime);
        //beat = songPos / secPerBeat;
        //beatOffset = GetAbsDistanceFromBeat(1);
        //^^^^ moved these to more direct getters.


        //If we've hit major beats (1, 0.5, 0.25) send out events.
        float beat = GetBeat();
        float dist = GetDistanceFromBeat(1, beat);
        if(!beatEnded1 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded1 = true;
            Global.Boss.EndOfBeat1();
            Global.Player.EndOfBeat1();
        } else if(dist < MINIMUM.thresholdAfterBeat) {//> 1 - MINIMUM.thresholdBeforeBeat) {
            //Debug.Log(dist - MINIMUM.thresholdAfterBeat);
            //if we've moved on to the next beat, open this flag 
            beatEnded1 = false;
        }

        dist = GetDistanceFromBeat(0.5f, beat);
        if(!beatEnded05 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded05 = true;
            Global.Boss.EndOfBeat05();
            Global.Player.EndOfBeat05();
        } else if(dist < MINIMUM.thresholdAfterBeat) {//> 0.5 - MINIMUM.thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded05 = false;
        }

        dist = GetDistanceFromBeat(0.25f, beat);
        if(!beatEnded025 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded025 = true;
            Global.Boss.EndOfBeat025(); 
            Global.Player.EndOfBeat025();
        } else if(dist < MINIMUM.thresholdAfterBeat) {//> 0.25 - MINIMUM.thresholdBeforeBeat) {
            //if we've moved on to the next beat, open this flag 
            beatEnded025 = false;
        }

    }

    //instead of tracker variables, use more direct getters. 
    //non-interpolated.
    public static float GetBeat() {
        double pos = AudioSettings.dspTime - songStartTime;
        return (float)(pos / secPerBeat);
    }

    //interpolates w/ Time.time if audio time hasn't changed between frames. 
    //may cause the beat to go backwards. 
    public static float GetBeatInterp() {
        double newPos = AudioSettings.dspTime - songStartTime;
        if(newPos != audioPos) {
            audioPos = newPos;
            timePos = Time.time;
            return (float)(newPos / secPerBeat);
        } else {
            double diff = Time.time - timePos;
            return (float)((newPos + diff) / secPerBeat);
        }
    }

    //ex. for 1, 5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.9
    public static float GetDistanceFromBeat() {
        return GetDistanceFromBeat(1);
    }
    public static float GetDistanceFromBeat(float fraction) {
        return GetDistanceFromBeat(fraction, GetBeat());
    }
    public static float GetDistanceFromBeat(float fraction, float beat) {
        return beat % fraction;
    }

    //ex. for 1, 5.1 returns 0.1, 5.5 returns 0.5, 5.9 returns 0.1
   /* public static float GetAbsDistanceFromBeat() {
        float b = GetDistanceFromBeat() - 0.5f;
        b = -Mathf.Abs(b);
        return b + 0.5f;
    }*/
    public static float GetAbsDistanceFromBeat() {
        return GetAbsDistanceFromBeat(1);
    }
    public static float GetAbsDistanceFromBeat(float fraction) {
        return GetAbsDistanceFromBeat(fraction, GetBeat());
    }
    public static float GetAbsDistanceFromBeat(float fraction, float beat) {
        float b = GetDistanceFromBeat(fraction, beat) - (fraction / 2);
        b = -Mathf.Abs(b);
        return b + (fraction / 2);
    }

    //for use by player actions. 
    //are we on beat, within the actionable threshold, according to a certain fraction?
    public static bool IsOnBeat() {
        return IsOnBeat(1);
    }
    public static bool IsOnBeat(float fraction) {
        return IsOnBeat(fraction, GetBeat());
    }
    public static bool IsOnBeat(float fraction, float beat) {
        
        float distFromBeat = GetDistanceFromBeat(fraction, beat);
        float distFromBeatAbs = GetAbsDistanceFromBeat(fraction, beat);

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
    public static Accuracy GetAccuracy() {
        return GetAccuracy(1);
    }
    public static Accuracy GetAccuracy(float fraction) {
        return GetAccuracy(fraction, GetBeat());
    }
    public static Accuracy GetAccuracy(float fraction, float beat) {

        float distFromBeat = GetDistanceFromBeat(fraction, beat);
        float distFromBeatAbs = GetAbsDistanceFromBeat(fraction, beat);

        //Debug.Log(distFromBeat);

        if(distFromBeat < fraction / 2) {
            //if after
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeat <= a.thresholdAfterBeat) {
                    return a;
                }
            }
            return TOO_LATE;
        } else {
            //if before 
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeatAbs <= a.thresholdBeforeBeat) {
                    return a;
                }
            }
            return TOO_EARLY;
        }
    }

    //Gets the nearest beat of this fraction, both before and after.
    public static float GetNearestBeat() {
        return GetNearestBeat(1);
    }
    public static float GetNearestBeat(float fraction) {
        return GetNearestBeat(fraction, GetBeat());
    }
    public static float GetNearestBeat(float fraction, float beat) {
        float dist = GetDistanceFromBeat(fraction, beat);
        if(dist > fraction / 2) {
            return beat + (1 - dist);
        } else {
            return beat - dist;
        }
    }
    

    //Like WaitForSeconds, but in sync with the music. 
    //USE THIS INSTEAD OF WAITFORSECONDS
    //similar code as below, would re-use by calling WaitForBeatsMulti(1) here but it's a coroutine so ? 
    public static IEnumerator WaitForBeat(float fraction) {
        
        float lastDistFromBeat = GetDistanceFromBeat(fraction);
        while(true) {
            yield return null;
            float newDistFromBeat = GetDistanceFromBeat(fraction);
            if(lastDistFromBeat > newDistFromBeat) {
                break;
            }
            lastDistFromBeat = newDistFromBeat;
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
