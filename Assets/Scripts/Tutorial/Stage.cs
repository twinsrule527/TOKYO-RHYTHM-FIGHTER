using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : TutorialStage
{

    public override void OnStageStart()
    {
        TutorialManager.SetDialogueFinished();
    }
    public override void StageConditionsMet()
    {
        Global.TutorialManager.NextStage();
    }

}
