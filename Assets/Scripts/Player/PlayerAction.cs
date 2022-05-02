using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    //beat fraction this action can be done on 
    //override TryAction for more complicated patterns 
    [SerializeField] protected float beatFraction = 1f;
    //How many beats the action takes to perform
    [SerializeField] protected float length = 1f;
    //what key, when pressed, makes this action happen
    //override CheckInput for other behavior that isn't a KeyCode press, this is just a useful default
    [SerializeField] protected KeyCode key;
    [SerializeField] protected ActionIndicator myActionIndicator;
    [SerializeField] private bool isComboable = true;
    public bool IsComboable {
        get {
            return isComboable;
        }
    }
    int comboCounter;   //may be used if needed 

    bool canInterrupt;  //do we call Boss.Interrupt()?


    public float damage;//How much damage this attack does
    public float baseDamage;//How much base damage this attack does

    
    //TODO: track when the player has done input for a beat and what type of beat. 
    //ex. locking down input however long needed 
    //both in fail and success 
    //TODO also want to increment the Player-wide combo counter 
    

    //Checks for input. ex. GetKeyPressed. 
    //This is here instead of in Update so that the Player can call this and control, for example,
    //what order they check in and what may or may not get checked for. 
    //Checks GetKeyDown by default, but can be overridden if other behavior wanted.
    //Should call TryAction(). 
    protected virtual void Start() {
        if(myActionIndicator == null) {
            myActionIndicator = GetComponentInChildren<ActionIndicator>();
        }
    }
    public virtual void CheckInput() {
        if(gameObject.activeInHierarchy) {
            if(Input.GetKeyDown(key)) {
                TryAction();
            }
        }

    }

    //This function gets called, for example, when the key tied to this action is pressed.
    //Can be overridden.
    protected virtual void TryAction() {

        if(Global.Player.CurrentAction == null) { //if we aren't locked down 
            
            //if we're on beat 
            Accuracy curAccuracy = BeatController.GetAccuracy(beatFraction);
            Global.Player.spriteController.DisplayAccuracy(curAccuracy);
            if(BeatController.IsOnBeat(beatFraction)) {//curAccuracy.priority > 0) {
                
                Success();
            }
            else {
                MessUp();
            }
        }
        else {
            MessUp();
            Global.Player.spriteController.DisplayMessup();
        }

    }


    //This function gets called when the player presses this key on beat. Carry out whatever action this is. 
    //Should be overridden and implemented with what pressing this key does.
    protected virtual void Success() {
        //On a success, increases the combo counter
        Global.ComboIndicator.IncrementCombo();
        //Debug.Log("Success, you're on beat");
        //Typically, sets the Player's current action to be this and plays an on beat sound
        Global.Player.sfxController.PlayOnBeatSound();
        Global.Player.CurrentAction = this;
        Global.Player.currActionEndBeat = BeatController.GetNearestBeat(beatFraction) + length;//Subtracts smallest beat fraction, so that it actually occurs before the beat, rather than after
        //Disables the next few BeatIndicators
        Global.BeatIndicatorBrain.DisablePlayerIndicators(Global.Player.currActionEndBeat);
        //call Interrupt on boss here 
        //Boss.InterruptAttack();
        if(canInterrupt) {
            Boss.InterruptAttack(this);
        }

        //TODO set the current player action to this 

    } 


    //This function gets called when the player presses this key, but it's off beat.
    //Should be overridden and implemented. 
    protected virtual void MessUp() {
        Global.ComboIndicator.SetCombo(0);
        Debug.Log("you messed up, you were off beat");
        Global.Player.CurrentAction = Global.Player.messUpAction;
        Global.Player.spriteController.MessUp();
        Global.Boss.SetBossVisualHP(Global.Boss.bossHP);
    }

    protected IEnumerator currentActionCoroutine;
    //This coroutine is performed so that the boss loses HP at the right moment, etc.
    public virtual IEnumerator ActionCoroutine() {
        yield return null;
    }
}
