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

    bool checkNextStage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkNextStage && BeatController.GetBeat() >= introBeats) {
            checkNextStage = false;
            Global.TutorialManager.NextStage();
        }
    }

    public override void OnStageStart() {
        
        //boss enabled.
        Global.Boss.GetComponent<SpriteRenderer>().enabled = true;
        Global.Boss.GetComponentInChildren<HurtAnimation>().hurtEnabled = true;

        //boss lerps in.
        //TODO

        introBeats = introBeats + Mathf.Ceil(BeatController.GetBeat());

        //switch the music.
        BeatController.SwitchSongContinuous(songToStart);
        checkNextStage = true;

    }

    public override void OnStageEnd() {
        Debug.Log("moved on from start gameplay stage");
    }
}
