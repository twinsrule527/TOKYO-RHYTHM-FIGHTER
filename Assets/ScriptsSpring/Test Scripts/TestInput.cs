using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A test program to make sure the Interrupt Function of the Boss Attacks works
public class TestInput : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) {
            Debug.Log(Global.Player.playerHealth);
        }
    }
}
