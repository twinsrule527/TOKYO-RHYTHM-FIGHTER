
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSync : MonoBehaviour
{

    public static void PlaySoundPerfect(AudioClip clip, AudioSource source, float beatNumber) {

        source.PlayOneShot(clip);

        //check if we're before or after the beat needed 

        //if before: 
        //use play at audio time 
        //calculate what audio time to play on using beat, bpm 

        //if after: 
        //calculate how many samples we need to cut off 
        //play from point in sample 
        //safeguard: if past end, dont 


    }
    
}
