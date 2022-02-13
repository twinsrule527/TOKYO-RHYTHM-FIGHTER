using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerAction[] actions;

    public int comboCounter;

    // Start is called before the first frame update
    void Start()
    {
        //load PlayerActions, which will be components on the Player object or its children 
        actions = GetComponentsInChildren(typeof(PlayerAction)) as PlayerAction[];

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
}
