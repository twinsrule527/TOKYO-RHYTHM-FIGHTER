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
        attackBag = new Bag<AttackPattern>();
        base.Start();
        //CreateAttackPatterns2();//COMMENT THIS OUT WHEN NEW VERSION IS READY
        attackBag.Refill();
        
    }
    //Called by Boss SongStarted
    public override void SongStarted()
    {
        base.SongStarted();
        RefillAttackQueue();
        StartCoroutine("StateUpdate");
    }

    //Creates all attack patterns - a temporary measure until we get a tool to do this working
        
    public void CreateAttackPatterns2() {
        List<BossAttack> newAttacks = new List<BossAttack>();
        //Attack Pattern 1: 2-Attack, 4-Attack, 1-Attack, 2-Wait
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[5]);
        newAttacks.Add(AttackChildren[2]);
        newAttacks.Add(AttackChildren[1]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "1"));
        
        //Attack Pattern 2: 2-Attack, 4-Attack, 1-Attack x4, 2-Wait
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[5]);
        for(int i = 0; i < 4; i++) {
            newAttacks.Add(AttackChildren[2]);
        }
        newAttacks.Add(AttackChildren[1]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "2"));
        
        //Attack Pattern 3: 3-Attack, 1-Attack x2, 2-Wait
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[4]);
        for(int i = 0; i < 2; i++) {
            newAttacks.Add(AttackChildren[2]);
        }
        newAttacks.Add(AttackChildren[1]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "3"));
        
        //Attack Pattern 4: 2-Attack, 2-Attack, 2-Wait
        newAttacks = new List<BossAttack>();
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[3]);
        newAttacks.Add(AttackChildren[1]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "4"));
        
        //Attack Pattern 5: 2-Attack, 1-Attack x2, 2-Attack, 1-Attack x2, 2-Wait
        newAttacks = new List<BossAttack>();
        for(int i = 0; i < 2; i++) {
            newAttacks.Add(AttackChildren[3]);
            newAttacks.Add(AttackChildren[2]);
            newAttacks.Add(AttackChildren[2]);
        }
        newAttacks.Add(AttackChildren[1]);
        attackBag.AddToLineup(new AttackPattern(newAttacks, this, "5"));
        
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
