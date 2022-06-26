using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDuringPause : MonoBehaviour
{
    void Start() {
        AudioSource [] audioSources = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource s in audioSources) {
            s.ignoreListenerPause = true;

            //https://answers.unity.com/questions/571482/audioignorelistenerpause.html lol 
            s.enabled = false;
            s.enabled = true;
        }
    }
}
