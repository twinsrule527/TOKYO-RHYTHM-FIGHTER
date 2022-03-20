using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{

    /*
        Stores data for each music track.
        These go on the SongData prefab, so that anywhere can pull from them. 
        A SongData object will be given to the BeatController to kick off that song. 
    */

    [SerializeField] private string _songName;
    public string songName { get; private set; }

    //the audio source holding the song to play. 
    [SerializeField] public AudioClip songAudioClip;

    //beats per minute of the song. 
    [SerializeField] private double _BPM;
    public double BPM { get; private set; }
    

    //delay at the beginning of file because information is encoded at the start.
    //TODO: is this in seconds, ms, samples, what? 
    //TODO: is this the same for all mp3s? do we need this?
    //528 samples appended to beginning / samples per second = fraction of second. (is this correct?)
    private static double _mp3Delay = 528 / 44100;  
    public double mp3Delay { get; private set; }

    //time before beat 0 in the song.
    //TODO: is this in seconds, ms, samples, what?
    [SerializeField] private double _songDelay;
    public double songDelay { get; private set; }

    void Start() {
        //so that these can be set in the inspector, but can't be changed by anything
        songName = _songName;
        BPM = _BPM;
        mp3Delay = _mp3Delay;
        songDelay = _songDelay;
    }
}
