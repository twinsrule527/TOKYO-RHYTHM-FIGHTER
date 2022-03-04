using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: This is an example of how the attack pattern coroutine system should work
//This is just a skeleton, subject to change/deletion
public class PlaytestBossAI : BossAI
{
    public GameObject Boss;
    
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
        newAttacks.Add(AttackChildren[0]);
        newAttacks.Add(AttackChildren[1]);
        //newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "1"));
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[1]);
        newAttacks.Add(AttackChildren[4]);
        newAttacks.Add(AttackChildren[2]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[0]);
        newAttacks.Add(AttackChildren[5]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "3"));

    }

    //Example attacks
    private IEnumerator ChargeAttack(int val) {
        mySprite.color = Color.blue;
        yield return new WaitForSeconds(bossBeat);
        mySprite.color = Color.white;
        yield return null;
        mySprite.color = Color.black;
    }

    public override IEnumerator Reposition(float amt = 0) {
        yield return null;
    }

    public override IEnumerator Death() {
        yield return null;
    }
}
