using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{

    int startDelay = 3;

    // Update is called once per frame
    void Update()
    {
        //wait a few frames to start the song so everything's synced 
        if(startDelay < 0) {
            Debug.Log(BeatController.GetBeat());
        } else if(startDelay > 0) {
            startDelay--;
        } else {
            startDelay = -1;
            BeatController.StartSong();
        }
    }
}
