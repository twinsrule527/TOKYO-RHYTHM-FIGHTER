using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A test program to make sure the Interrupt Function of the Boss Attacks works
public class TestInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Boss.CurrentBoss.AttackAI.CurrentAttack.Interrupt();
        }
    }
}
