using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Subclass of BossAI for the first Boss
public class A_BossAI : BossAI
{
    //Temporary AttackPattern Construction
    [SerializeField] protected List<BossAttack> AttackChildren;
    
    [SerializeField] private float bossBeat;//The beat that this goes along with
    [SerializeField] private SpriteRenderer mySprite;//This will eventually go in a separate object
    void Start() {
        attackBag = new Bag<AttackPattern>();
        CreateAttackPatterns();
        attackBag.Refill();
        StartCoroutine("StateUpdate");
    }

    //Creates all attack patterns - a temporary measure until we get a tool to do this working
    public void CreateAttackPatterns() {
        List<BossAttack> newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[1]);
        newAttacks.Add(AttackChildren[0]);
        //newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "1"));
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[2]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));

    }

    public override IEnumerator Death() {
        yield return null;
    }
}
