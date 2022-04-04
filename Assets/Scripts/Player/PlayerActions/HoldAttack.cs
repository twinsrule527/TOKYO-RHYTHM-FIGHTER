using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAttack : PlayerAction

{
    public float damage;//How much damage this attack does

    bool isHolding = true; //check if player is holding the key

    IEnumerator currentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
//disable BaseAction when testing holdAttack?: Set to seperate key input for test (C)
//do i need to override the checkInput or just need to reference it?: override checkInput to change input conditions

    
    public override void CheckInput() {
        
        base.CheckInput();

        if(isHolding){
            if(!Input.GetKey(key)){
                isHolding = false;//DEBUG
                Debug.Log("isHolding = false line 55");
            }
        }

    }
    
    protected override void TryAction() {

        if(Global.Player.CurrentAction == null) { //if we aren't locked down

            //if we're on beat 
            Accuracy curAccuracy = BeatController.GetAccuracy(beatFraction);
            Global.Player.spriteController.DisplayAccuracy(curAccuracy);
            if(BeatController.IsOnBeat(beatFraction)) {//curAccuracy.priority > 0) {
                
                //start a courotine called hold that checks hold for certain amount of beats before success
                //if(isHolding == true){
                    //Success();
                //}

                Success();
                Debug.Log("Success() line 77");
                
            }
            else {
                MessUp();
            }
        }

    }

    protected override void Success()
    {
        base.Success();
       // if(!Global.Boss.makeAttackThisBeat) {//DOesn't always work correctly
            //Global.Boss.ChangeBossHP(-damage);
            //Global.Player.spriteController.Attack(1);
        /*}
        else {
            //Play the MessUp/Hurt Animation
        }*/

        currentCoroutine = HoldCoroutine();
        StartCoroutine(currentCoroutine);
    }




    public IEnumerator HoldCoroutine() {
        float t = 0;
        float startTime = BeatController.GetBeat();
        Global.Player.spriteController.Attack(1);
        isHolding = true;
//DEBUG
        while(t < startTime + 2 * Time.deltaTime){
            t = BeatController.GetBeat();

            Global.Boss.ChangeBossHP(-damage);

            if(isHolding != true) {
                //break;
                MessupHold();
                Debug.Log("isHolding != true line 113");
            }
            yield return null;
        }  

    }
    //have a function that controls HoldCourotine
    void MessupHold(){//DEBUG
        StopCoroutine(currentCoroutine);
        //Debug.Log("MessupHold() line 125");
    }

    //order fix: Put success at the beginning, put startCourotine in success override, and put change boss hp towards the end
}
