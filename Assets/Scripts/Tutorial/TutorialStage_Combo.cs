using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage_Combo : TutorialStage
{
    //This tutorial stage has an update function where it checks to see if the player has a great enough combo
    //If they do, the button shows up
    [SerializeField] private int comboRequired;
    private bool stageCompleted;
    void Update() {
        if(!stageCompleted) {
            Debug.Log("going");
            if(ComboIndicator.comboCounter > comboRequired) {
                NextStageButton.SetActive(true);
                stageCompleted = true;
                Debug.Log("COMPLETED");
            }
        }
    }
}
