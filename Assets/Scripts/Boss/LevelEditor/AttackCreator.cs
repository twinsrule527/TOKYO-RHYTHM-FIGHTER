using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Input script that outputs the attack patterns for a boss
public static class AttackCreator
{
    public static void CreateAttackPatterns(List<List<char>> atks, BossAI myBoss) {
        Bag<AttackPattern> bossBag = myBoss.attackBag;
        //Gets a dictionary, for which it will assign all attacks
        Dictionary<char, BossAttack> atkKeys = new Dictionary<char, BossAttack>();
        List<BossAttack> bossAttacks = new List<BossAttack>(myBoss.GetComponentsInChildren<BossAttack>());
        foreach(BossAttack atk in bossAttacks) {
            atkKeys.Add(atk.CreatorKey, atk);
        }
        //Goes through each list of chars, making it into an attack pattern
        for(int i = 0; i < atks.Count; i++ ) {
            List<char> currentPattern = atks[i];
            List<BossAttack> newAttacks = new List<BossAttack>();
            for(int j = 0; j < currentPattern.Count; j++) {
                //If the corresponding key is not in the attack pattern, it returns null
                if(atkKeys.ContainsKey(currentPattern[j])) {
                    newAttacks.Add(atkKeys[currentPattern[j]]);
                }
                else {
                    //Shoots out which attack is missing
                    Debug.Log("missed Key: Pattern " + i + ", Atk " + j);
                }
            }
            bossBag.AddToLineup(new AttackPattern(newAttacks, myBoss, i.ToString()));
        }
    }

    public static void CreateAttackPatterns(List<string> atks, BossAI myBoss) {
        List<List<char>> atksNew = new List<List<char>>();
        for(int i = 0; i < atks.Count; i++) {
            List<char> pattern = new List<char>(atks[i]);
            atksNew.Add(pattern);
        }
        CreateAttackPatterns(atksNew, myBoss);
    }
}
