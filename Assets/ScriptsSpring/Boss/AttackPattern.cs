using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Attack {
    public string name;
    public float val;

    public Attack(string Name, float Val) {
        name = Name;
        val = Val;
    }
}

public class AttackPattern
{

    /*
        Boss attack pattern 
        ft. custom Equals function for compare anywhere 
        !!! these will compare by Name by string compare !!! 

    */

    public List<BossAttack> coroutines { get; private set; }
    public BossAI CoroutineSource { get; private set; }
    public string name { get; private set; }

    public AttackPattern(List<BossAttack> attackCoroutine, BossAI attackSource, string attackName) {
        coroutines = attackCoroutine;
        CoroutineSource = attackSource;
        name = attackName;
    }
/*
    public IEnumerator StartAttacks() {
        for(int i = 0; i < coroutines.Count; i++) {
            yield return CoroutineSource.StartCoroutine(coroutines[i]);
        }
    }*/

    public override bool Equals(System.Object obj) {

        if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
            return false;
        }
        else {
            return name.Equals(((AttackPattern)obj).name);
        }
    }
}
