
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSync : MonoBehaviour
{

    public static bool soundEffectsEnabled = true;

    public static void PlaySoundPerfect(AudioClip clip, AudioSource source, float beatNumber) {

        if(!soundEffectsEnabled) {
            return;
        }

        //TODO
        //this function would have made the sound effects play perfectly on beat
        //like in rez
        //fudge it a bit so it sounds good
        //but this was out of scope for the deadline, so instead we just play stuff when it happens. 
        source.PlayOneShot(clip);

        /*
        //check if we're before or after the beat needed 
        float beat = BeatController.GetBeat();
        if(beat < beatNumber) {
            //we're before the beat

            //if before: 
            //use play at audio time 
            //calculate what audio time to play on using beat, bpm 

        } else {
            //we're after the beat

            //calculate how many samples we need to cut off 
            //play from point in sample 
            //safeguard: if past end, dont, and print error

        }
        */

    }
    
}
