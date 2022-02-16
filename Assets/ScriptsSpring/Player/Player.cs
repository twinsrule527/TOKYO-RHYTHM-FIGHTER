using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerAction[] actions;
    public MessUpAction messUpAction;//The player has a mess-up action that their current action gets set to if they fail to perform an action

    public int comboCounter;

    public PlayerAction CurrentAction;//The action the player is currently performing
    
    //How much health the player starts with
    [SerializeField] private float playerStartHealth;
    public float playerHealth {get; private set;}

    // Start is called before the first frame update
    void Start()
    {   
        //There can only be 1 player, and it will be the Instance of the player
        Global.Player = this;
        playerHealth = playerStartHealth;
        //load PlayerActions, which will be components on the Player object or its children 
        actions = GetComponentsInChildren<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for input that might activate a PlayerAction
        //we can control the order these are executed in here if we need to 
        foreach(PlayerAction a in actions) {
            a.CheckInput();
        }
    }

    //How outside objects should affect the player's health
    public void ChangeHP(float amt = 0) {
        playerHealth += amt;
        Global.UIManager.SetHealthText();
        if(playerHealth <= 0) {
            GameManager.PlayerLoses();
        }
    }

    public void EndOfBeat1() {
        CurrentAction = null; //TODO bandaid fix 
    }

    public void EndOfBeat05() {

    }

    public void EndOfBeat025() {

    }



}
