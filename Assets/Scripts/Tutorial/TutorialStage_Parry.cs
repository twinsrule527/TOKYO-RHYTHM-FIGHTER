using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage_Parry : TutorialStage
{
    //This tutorial stage should have a method to check when the player parries successfully

    [SerializeField] private int parryNumRequired;
    private static bool stageCompleted;
    private static int currParryNum;
    [SerializeField] private int stageNumber;

    void Start() {
        currParryNum = 0;
    }
    void Update() {
        if(!stageCompleted && GameManager.currentStage == stageNumber) {
            if(currParryNum > parryNumRequired) {
                stageCompleted = true;
            }
        }
    }

    //A way for any object to increase the number of parries that have been carried out - called by all bossAttacks when Parried
    public static void IncreaseParryNumber() {
        if(!stageCompleted) {
            currParryNum++;
        }
    }

    public override bool CheckStageChange() {
        return stageCompleted;
    }

}
