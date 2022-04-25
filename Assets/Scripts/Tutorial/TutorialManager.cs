using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script manages the general tutorial, such as determining what stage of the tutorial the player is on
public class TutorialManager : MonoBehaviour
{
    private int _tutorialStage;//What stage of the tutorial we are on
        //Stage 0: Intro to Acting - Parry
            //Stage 1: Intro to Base Attack
                //Stage 2: Intro to Hold Attack
    [SerializeField] private int finalStage;//Number of the last stage
    [SerializeField] private GameObject TutorialBoss;//The Boss for for the tutorial
    [SerializeField] private GameObject RealBoss;//The Boss for after the tutorial
    public int TutorialStage {
        get {
            return _tutorialStage;
        }
    }

    [SerializeField] private List<TutorialStage> Stages;//A list of stages of the tutorial

    void Start() {
        Global.TutorialManager = this;
        Global.Tutorial = true;
        SetObjects(Stages[_tutorialStage], true);
    }

    //Moves to the next stage of the tutorial
    public void NextStage() {
        //Resets all objects changed by the current stage
        SetObjects(Stages[_tutorialStage], false);
        Stages[_tutorialStage].NextStageButton.SetActive(false);
        _tutorialStage++;
        GameManager.currentStage = _tutorialStage;
        Global.Boss.AttackAI.StartStage(_tutorialStage);
        if(_tutorialStage > finalStage) {
            EndTutorial();
            return;
        }
        //Sets new objects in accordance with the new stage
        SetObjects(Stages[_tutorialStage], true);
        
    }
    //Sets up the next stage of the tutorial, which the player is then sent to when they press a button
    public void SetUpNextStage() {
        Stages[_tutorialStage].NextStageButton.SetActive(true);
    }
    public void EndTutorial() {
        Global.Tutorial = false;
    }

    //This function activates/deactivates the correct gameObjects whenever a tutorial stage changes
        //Give it the stage that is being chosen, and whether said stage is activating or deactivating
    private void SetObjects(TutorialStage stage, bool active) {
        foreach(GameObject obj in stage.DeactivatedObjects) {
            obj.SetActive(!active);
        }
        foreach(GameObject obj in stage.ActivatedObjects) {
            obj.SetActive(active);
        }
    }
}
