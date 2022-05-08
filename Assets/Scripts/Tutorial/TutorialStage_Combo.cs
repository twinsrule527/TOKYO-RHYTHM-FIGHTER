using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage_Combo : TutorialStage
{
    //This tutorial stage has an update function where it checks to see if the player has a great enough combo
    //If they do, the button shows up
    [SerializeField] private int comboRequired;
    private bool stageCompleted;
    [SerializeField] private int stageNumber;
    void Update() {
        if(!stageCompleted && GameManager.currentStage == stageNumber) {
            if(Global.ComboIndicator.GetCombo() > comboRequired) {
                NextStageButton.SetActive(true);
                stageCompleted = true;
            }
        }
    }

    public override void OnStageStart()
    {
        base.OnStageStart();
        Global.ComboIndicator.SetCombo(0);
    }
}
