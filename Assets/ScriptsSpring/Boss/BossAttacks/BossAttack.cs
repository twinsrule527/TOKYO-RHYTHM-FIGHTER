using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enum is used for InterruptInputs, to see what general type of input was given
public enum InputType {
    Attack,
    Block
}
//This is a struct that has all the information an Interrupt function might need 
public struct InterruptInput {
    public InputType input;
    public float amtOnBeat;//How on beat
    public InterruptInput NullInput() {
        InterruptInput newInput;
        newInput.input = InputType.Attack;
        newInput.amtOnBeat = 0;
        return newInput;
    }
}

//This is an abstract class which manages a single attack that a boss can make
    //It contains 4 very important functions:
        //An Attack coroutine that runs through the attack
        //An Interrupt function in case the attack can be interrupted
        //A Cancel coroutine that the Interrupt function runs to stagger the boss when they're interrupted
        //A CheckAttackSuccess Coroutine which checks to see if the attack hits after the beat leeway time passes
public abstract class BossAttack : MonoBehaviour
{

    public abstract IEnumerator Attack();

    public abstract void Interrupt(PlayerAction action);

    public abstract IEnumerator Cancel();

    public abstract void CheckAttackSuccess();
}
