using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern
{

    /*
        Boss attack pattern 
        ft. custom Equals function for compare anywhere 
        !!! these will compare by Name by string compare !!! 

    */

    Coroutine coroutine;

    public string name { get; private set; }

    public AttackPattern(Coroutine attackCoroutine, string attackName) {
        coroutine = attackCoroutine;
        name = attackName;
    }

    public override bool Equals(System.Object obj) {

        if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
            return false;
        }
        else {
            return name.Equals(((AttackPattern)obj).name);
        }
    }
}
