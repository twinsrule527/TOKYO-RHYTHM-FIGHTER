using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_StartGameplay : Stage
{

    //Start the music.
    //Give the player a few beats to let the song start.
    //Then, kick off gameplay.

    [SerializeField] SongData songToStart;

    public float introBeats = 16;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BeatController.GetBeat() >= introBeats) {
            Global.TutorialManager.NextStage();
        }
    }

    public override void OnStageStart() {
        
        //boss enabled.
        Global.Boss.GetComponent<SpriteRenderer>().enabled = true;

        //boss lerps in.
        //TODO

        //switch the music.
        BeatController.StartSong(songToStart);

    }

    public override void OnStageEnd() {

    }
}
