using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is an abstract class which manages a single attack that a boss can make
    //It contains 3 very important functions:
        //An Attack coroutine that runs through the attack
        //An Interrupt function in case the attack can be interrupted
        //A Cancel coroutine that the Interrupt function runs to stagger the boss when they're interrupted
public abstract class BossAttack : MonoBehaviour
{

    public abstract IEnumerator Attack();

    public abstract void Interrupt();

    public abstract IEnumerator Cancel();
}
