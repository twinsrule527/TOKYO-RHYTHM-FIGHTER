using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    //beat fraction this action can be done on 
    //override TryAction for more complicated patterns 
    [SerializeField] protected float beatFraction = 1f;    

    //what key, when pressed, makes this action happen
    //override CheckInput for other behavior that isn't a KeyCode press, this is just a useful default
    [SerializeField] protected KeyCode key; 

    int comboCounter;   //may be used if needed 

    bool canInterrupt;  //do we call Boss.Interrupt()?

    //TODO: track when the player has done input for a beat and what type of beat. 
    //ex. locking down input however long needed 
    //both in fail and success 
    //TODO also want to increment the Player-wide combo counter 
    

    //Checks for input. ex. GetKeyPressed. 
    //This is here instead of in Update so that the Player can call this and control, for example,
    //what order they check in and what may or may not get checked for. 
    //Checks GetKeyDown by default, but can be overridden if other behavior wanted.
    //Should call TryAction(). 
    public void CheckInput() {
        
        if(Input.GetKeyDown(key)) {
            TryAction();
        }

    }

    //This function gets called, for example, when the key tied to this action is pressed.
    //Can be overridden.
    protected virtual void TryAction() {

        if(BeatController.IsOnBeat(beatFraction)) {
            Success();
        } else {
            MessUp();
        }

    }


    //This function gets called when the player presses this key on beat. Carry out whatever action this is. 
    //Should be overridden and implemented with what pressing this key does.
    protected virtual void Success() {

        Debug.Log("Success, you're on beat");
        //Typically, sets the Player's current action to be this
        Global.Player.CurrentAction = this;

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

        Debug.Log("you messed up, you were off beat");
        Global.Player.CurrentAction = Global.Player.messUpAction;

    }
}
