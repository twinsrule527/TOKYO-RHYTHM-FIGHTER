using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is an abstract class which manages a single attack that a boss can make
    //It contains two very important functions:
        //A coroutine that runs through the attack
        //An interrupt function in case the attack can be interrupted
public abstract class BossAttack : MonoBehaviour
{
    public abstract IEnumerator Attack();

    public abstract void Interrupt();
}
