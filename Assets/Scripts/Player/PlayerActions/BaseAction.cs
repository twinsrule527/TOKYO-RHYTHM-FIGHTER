using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A starting action in the game, which works as both an attack & a parry
public class BaseAction : PlayerAction
{
    public float damage;//How much damage this attack does

    

    // Update is called once per frame
    void Update()
    {
    }

/*
    protected override void TryAction()
    {
        
        if(Global.Player.CurrentAction == null) {
            Accuracy curAccuracy = BeatController.GetAccuracy(beatFraction);
            //Call PlayerSpriteController.DisplayAccuracy(Accuracy);
            if(curAccuracy.priority > 0) {
                    Success();
            }
            else {
                MessUp();
            }
        }
        

    }
    */
//Override succcess function if you need to override the function from the original script
    protected override void Success()
    {
        //myActionIndicator.gameObject.SetActive(true);
        //myActionIndicator.PerformAction();
        if(Global.Boss.AttackAI.AttackBeatHitOn != BeatController.GetNearestBeat() || !Global.Boss.AttackAI.CurrentAttackOutgoing.IsAnAttack) {//DOesn't always work correctly
            Global.Player.spriteController.Attack(1);
            currentActionCoroutine = ActionCoroutine();
            StartCoroutine(currentActionCoroutine);
            base.Success();
        
        }
        else {
            //Play the MessUp/Hurt Animation
            Global.Player.ChangeHP();
        }
    }
    protected override void MessUp()
    {
        base.MessUp();
        if(currentActionCoroutine != null) {
            StopCoroutine(currentActionCoroutine);
        }
    }
    public override IEnumerator ActionCoroutine()
    {
        //Deals damage at the end of the action, in case it get messed up
        float startTime =  BeatController.GetBeat();
        float t = startTime;
        while(t < startTime + length - 0.5f) {
            yield return null;
            t = BeatController.GetBeat();
        }
        Debug.Log("DAMAGED");
        Global.Boss.ChangeBossHP(-damage);
    }
}
