using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: This is an example of how the attack pattern coroutine system should work
//This is just a skeleton, subject to change/deletion
public class PlaytestBossAI : BossAI
{
    public GameObject Boss;
    [SerializeField] private float bossBeat;//The beat that this goes along with
    [SerializeField] private SpriteRenderer mySprite;//This will eventually go in a separate object
    void Start() {
        attackBag = new Bag<AttackPattern>();
        attackBag.AddToLineup(AttackPattern1());
        attackBag.Refill();
        StartCoroutine("StateUpdate");
    }

    
    //Each rhythmic set of attacks is compiled in a single attackpattern
    public AttackPattern AttackPattern1() {
        List<Attack> attacks = new List<Attack>();
        attacks.Add(new Attack("Reposition", -1));
        //attacks.Add(new Attack("DashAttack", 0));
        attacks.Add(new Attack("Wait",0.5f));
        //attacks.Add(new Attack("DashAttack",1));
        attacks.Add(new Attack("FastAttack", -1));
        return new AttackPattern(attacks, this, "atk1");
    }

    //An example attack coroutine, where the boss dashes across the screen
    [SerializeField] private float DashAttack_DashTime;
    [SerializeField] private float DashAttack_DashDistance;

    private IEnumerator DashAttack(float directionFloat) {
        Vector3 direction;
        if(directionFloat == 0) {
            direction = transform.right;
        }
        else {
            direction = -transform.right;
        }
        Vector3 startPosition = Boss.transform.position;
        Vector3 endPosition = Boss.transform.position + direction * DashAttack_DashDistance;
        float curTime = 0;
        //At the start, it also starts an animation
        while(curTime < DashAttack_DashTime) {
            curTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, curTime / DashAttack_DashTime);
            yield return null;
        }
        transform.position = endPosition;
    }

    //Three different attacks, to test our rhythm system
    private IEnumerator FastAttack(int val) {
        mySprite.color = Color.red;
        yield return new WaitForSeconds(bossBeat);
        mySprite.color = Color.white;
        yield return null;
        mySprite.color = Color.black;
    }

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
