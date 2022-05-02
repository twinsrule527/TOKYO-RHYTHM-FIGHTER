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
    public float thresholdBeforeBeat;
    public float thresholdAfterBeat;

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

    [SerializeField] bool isGameScene = true;

    //non-static reference to the BeatController in scene 
    //used to call coroutines
    static BeatController instance;

    public static bool isPlaying { get; private set; }

    //non-static so it'll get reset when scene changes or is reloaded
    public bool songPaused  { get; private set; }
    private bool pausedOutsideMenu = false;


    //accuracy thresholds, in seconds 
    //will be converted into fractions of beats on start. 
    static float secondsMINIMUM = 0.20f;
    static float secondsGREAT = 0.13f;
    static float secondsPERFECT = 0.07f;


    //// Beat accuracies! 
    //You might get these from BeatController functions.
    //You can check them against each other
    //ex. if(accuracyPassedIn.Equals(BeatController.PERFECT)) { do something cause it's perfect }

    private static Accuracy minimum = new Accuracy(0.13f, 0.13f, "OK", 1);
    public static Accuracy MINIMUM {
        get { return minimum; }
    }
    private static Accuracy great = new Accuracy(0.08f, 0.08f, "GREAT", 5);
    public static Accuracy GREAT {
        get { return great; }
    }

    private static Accuracy perfect = new Accuracy(0.04f, 0.04f, "PERFECT", 9);
    public static Accuracy PERFECT {
        get { return perfect; }
    }
    private static Accuracy too_early = new Accuracy(float.NaN, float.NaN, "TOO EARLY", -1);
    public static Accuracy TOO_EARLY {
        get { return too_early; }
    }
    private static Accuracy too_late = new Accuracy(float.NaN, float.NaN, "TOO LATE", -2);
    public static Accuracy TOO_LATE {
        get { return too_late; }
    }

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
    private static double baselineBeat = 0;

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


    void Awake() {
        instance = this;
    }


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

    public static void SwitchSongContinuous(SongData songToStart) {
        instance.StartCoroutine(instance.SwitchClean(songToStart));
    }
    IEnumerator SwitchClean(SongData songToStart) {
        yield return WaitForBeat(1);
        baselineBeat = GetNearestBeat();
        StartSong(songToStart);
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

    static float SecondsToBeatFrac(float seconds) {
        return seconds * ((float)(BPM) / 60f);
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

        Debug.Log("original minimum: " + MINIMUM.thresholdBeforeBeat);

        //set up thresholds based on BPM.
        //given thresholds in seconds, what's the threshold in beat fractions?
        minimum.thresholdBeforeBeat = SecondsToBeatFrac(secondsMINIMUM);
        minimum.thresholdAfterBeat = SecondsToBeatFrac(secondsMINIMUM);
        great.thresholdBeforeBeat = SecondsToBeatFrac(secondsGREAT);
        great.thresholdAfterBeat = SecondsToBeatFrac(secondsGREAT);
        perfect.thresholdBeforeBeat = SecondsToBeatFrac(secondsPERFECT);
        perfect.thresholdAfterBeat = SecondsToBeatFrac(secondsPERFECT);

        accuraciesToCheck[0] = perfect;
        accuraciesToCheck[1] = great;
        accuraciesToCheck[2] = minimum;

        Debug.Log("new minimum: " + MINIMUM.thresholdBeforeBeat);
        Debug.Log("minimum in array: " + accuraciesToCheck[2].thresholdBeforeBeat);

        //set the audio source audio clip to this song 
        audioSource.clip = songData.songAudioClip;

        //set a start time just in case other stuff needs it 
        songStartTime = AudioSettings.dspTime + songData.mp3Delay + songData.songDelay;
        //set off all stuff related to the song playing- lag spike here 
        GameManager.SongStarted(songData);

        audioSource.Play();
        //kick off tracker with current time after lag spike
        songStartTime = AudioSettings.dspTime + songData.mp3Delay + songData.songDelay;

        UnPauseOutsideMenu();

        isPlaying = true;

    }

    
    //pause menu has been brought up.
    public static void PauseByMenu() {
        instance.pausedOutsideMenu = instance.songPaused;
        Pause();
    }

    //pause menu closed.
    public static void UnPauseByMenu() {
        if(!instance.pausedOutsideMenu) {
            UnPause();
        }
    }

    public static void PauseOutsideMenu() {
        instance.pausedOutsideMenu = true;
        Pause();
    }
    public static void UnPauseOutsideMenu() {
        instance.pausedOutsideMenu = false;
        UnPause();
    }

    //pause the beat and music. DO NOT CALL DIRECTLY call FromMenu/OutsideMenu instead
    private static void Pause() {
        instance.songPaused = true;
        AudioListener.pause = true;
    }

    //unpause the beat and music. 
    private static void UnPause() {
        AudioListener.pause = false;
        instance.songPaused = false;
    }

    //stops everything from 
    static double secToSlowFail = 3d;
    public static void FailStop() {
        instance.StartCoroutine(instance.SlowToStop(true, secToSlowFail));
    }

    //slow to stop, but tuned differently cause... you won! 
    //like slow to stop but only changes the beat, not the actual audio time. 
    //TODO-- could have it switch intelligently to the ending sample here, or another func for general switching 
    static double secToSlowWin = 4d;
    public static void WinStop() {
        instance.StartCoroutine(instance.SlowToStop(false, secToSlowWin));
    }

    //TODO: ummmmmm this causes weird stuff like repeating sfx on win. what do 
    //maybe disable being able to play sounds or smth?
    IEnumerator SlowToStop(bool fail, double secToSlowToStop) {

        double timeLeft = secToSlowToStop;
        float lastBeat = GetBeat() - (float)baselineBeat;

         while(1 == 1) {

            timeLeft -= Time.deltaTime;

            if(timeLeft > 0) {

                double percent = (timeLeft / secToSlowToStop);

                //wind down speed of audio via pitch
                if(fail) {
                    audioSource.pitch = (float)percent;
                }

                //wind down speed of beat and things dependent on it
                double currentTime = AudioSettings.dspTime - songStartTime;
                secPerBeat = ((currentTime / lastBeat) - secPerBeat) * (1 - percent) + secPerBeat;

            } else {

                //the frame we pass 0 

                PauseOutsideMenu();

                isPlaying = false;
                //audioSource.Stop();

                if(fail) {
                    
                    GameManager.PlayerLosesFinish();

                } else if(!fail) {

                    //TODO this is a placeholder for some transition idk 
                    //because we want to let it play out the uhhh ending of the song where it like wrrrrrrrrr final note 
                    
                    GameManager.PlayerWinsFinish();
                }
                
            }

            lastBeat = GetBeat();

            yield return null;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update all our tracker variables. 
        //songPos = (float)(AudioSettings.dspTime - songStartTime);
        //beat = songPos / secPerBeat;
        //beatOffset = GetAbsDistanceFromBeat(1);
        //^^^^ moved these to more direct getters.

        if(!isGameScene) {
            return;
        }


        //If we've hit major beats (1, 0.5, 0.25) send out events.
        float beat = GetBeat();
        float dist = GetDistanceFromBeat(1, beat);
        if(!beatEnded1 && dist > MINIMUM.thresholdAfterBeat) {
            beatEnded1 = true;
            Global.Boss.EndOfBeat1();
            Global.Player.EndOfBeat1();
        } else if(dist < MINIMUM.thresholdAfterBeat) {//> 1 - MINIMUM.thresholdBeforeBeat) {
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
        return (float)(baselineBeat + (pos / secPerBeat));
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
    public static float GetDistanceFromBeat(float fraction = 1) {
        //return GetDistanceFromBeat(fraction, GetBeat());
        return GetBeat() % fraction;
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
    public static float GetAbsDistanceFromBeat(float fraction = 1) {
        return GetAbsDistanceFromBeat(fraction, GetBeat());
    }
    public static float GetAbsDistanceFromBeat(float fraction, float beat) {
        float b = GetDistanceFromBeat(fraction, beat) - (fraction / 2);
        b = -Mathf.Abs(b);
        return b + (fraction / 2);
    }

    //for use by player actions. 
    //are we on beat, within the actionable threshold, according to a certain fraction?
    public static bool IsOnBeat(float fraction = 1) {
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
    public static Accuracy GetAccuracy(float fraction = 1) {
       
        return GetAccuracy(fraction, GetBeat());
    }
    public static Accuracy GetAccuracy(float fraction, float beat) {

        float distFromBeat = GetDistanceFromBeat(fraction, beat);
        float distFromBeatAbs = GetAbsDistanceFromBeat(fraction, beat);

        if(distFromBeat < fraction / 2) {
            //if after
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeat <= a.thresholdAfterBeat) {
                    return a;
                }
            }

            //ComboIndicator.comboCounter = 0;
            return TOO_LATE;
            
        } else {
            //if before 
            foreach(Accuracy a in accuraciesToCheck) {
                if(distFromBeatAbs <= a.thresholdBeforeBeat) {
                    return a;
                }
            }

            //ComboIndicator.comboCounter = 0;
            return TOO_EARLY;
        }

    }

    //Gets the nearest beat of this fraction, both before and after.
    public static float GetNearestBeat(float fraction = 1) {
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
