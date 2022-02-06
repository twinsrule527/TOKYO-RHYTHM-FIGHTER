using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Boss AI Superclass - uses a state machine w/ coroutines
    //Manages Boss health and attack patterns
        //Subclasses are implementations of each boss with actual attack pattern
public class BossAI : MonoBehaviour
{
    protected float bossHP;//Design question: should this be an integer?
    
    public void ChangeBossHP(int amt) {//Function to be called by others when increasing/decreasing hp
        bossHP += amt;
    }
}
