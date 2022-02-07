using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Boss AI Superclass - uses a state machine w/ coroutines
    //Manages Boss attack patterns
        //Subclasses are implementations of each boss with actual attack pattern
public class BossAI : MonoBehaviour
{
    //TODO: Move to Boss Class
    protected float bossHP;//Design question: should this be an integer?
    public void ChangeBossHP(int amt) {//Function to be called by others when increasing/decreasing hp
        bossHP += amt;
    }

    protected Bag<AttackPattern> attackBag;
    
    //All bosses have a Coroutine to just wait
    public IEnumerator Wait(float amt = 0) {
        yield return new WaitForSeconds(amt);
    }

    public IEnumerator Reposition() {
        yield return null;   
    }
}
