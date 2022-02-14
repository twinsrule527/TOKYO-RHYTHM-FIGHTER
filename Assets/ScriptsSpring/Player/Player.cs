using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerAction[] actions;

    public int comboCounter;
    
    //How much health the player starts with
    [SerializeField] private float playerStartHealth;
    public float playerHealth {get; private set;}

    // Start is called before the first frame update
    void Start()
    {   
        //There can only be 1 player, and it will be the Instance of the player
        Global.Player = this;
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
    }
}
