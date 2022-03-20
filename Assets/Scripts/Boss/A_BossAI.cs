using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Subclass of BossAI for the first Boss
public class A_BossAI : BossAI
{
    //Temporary AttackPattern Construction
    [SerializeField] protected List<BossAttack> AttackChildren;//0 = Wait 1, 1 = LongAttack(4), 2 = ShortAttack(2), 3 = OneBeatAttack(1)
    
    [SerializeField] private float bossBeat;//The beat that this goes along with
    [SerializeField] private SpriteRenderer mySprite;//This will eventually go in a separate object
    public override void Start() {
        base.Start();
        attackBag = new Bag<AttackPattern>();
        CreateAttackPatterns();
        attackBag.Refill();
        RefillAttackQueue();
        StartCoroutine("StateUpdate");
    }

    //Creates all attack patterns - a temporary measure until we get a tool to do this working
    public void CreateAttackPatterns() {
        List<BossAttack> newAttacks = new List<BossAttack>();
        //Attack Pattern 1: 4-Attack, 1-Attack, 2-Wait
        newAttacks.Add(AttackChildren[1]);
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[0]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "1"));
        
        //Attack Pattern 2: 4-Attack, 1-Attack x5, 2-Wait
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[1]);
        for(int i = 0; i < 5; i++) {
            newAttacks.Add(AttackChildren[3]);
        }
        newAttacks.Add(AttackChildren[0]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));
        
        //Attack Pattern 3: 1-Attack, 1-Attack, 1-Attack, 1-Wait
        newAttacks = new List<BossAttack>();
        for(int i = 0; i < 3; i++) {
            newAttacks.Add(AttackChildren[3]);
        }
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));
        
        //Attack Pattern 4: 1-Attack, 2-Attack, 1-Wait
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[1]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));
        
        /*newAttacks.Add(AttackChildren[1]);
        newAttacks.Add(AttackChildren[0]);
        //newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "1"));
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[2]);
        newAttacks.Add(AttackChildren[0]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));*/

    }

    public override IEnumerator Death() {
        yield return null;
    }
}
