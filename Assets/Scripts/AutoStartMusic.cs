using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartMusic : MonoBehaviour
{
    [SerializeField] int startDelay = 5;

    // Update is called once per frame
    void Update()
    {
        //wait a few frames to start the song so everything's synced 
        if(startDelay < 0) {
        } else if(startDelay > 0) {
            startDelay--;
        } else {
            startDelay = -1;
            BeatController.StartSong();
        }
    }
}
