using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: This is an example of how the attack pattern coroutine system should work
//This is just a skeleton, subject to change/deletion
public class BossAttackSkeleton : MonoBehaviour
{
    public GameObject Boss;
    void Start() {
        StartCoroutine("AttackPattern");
    }

    //Each rhythmic set of attacks is compiled in a single attackpattern
    public IEnumerator AttackPattern() {
        //At the start of the attack pattern, the boss repositions itself to the right spot
        yield return StartCoroutine("Reposition");
        //Then, they perform a series of attacks in a row
        yield return StartCoroutine(DashAttack(transform.right));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(DashAttack(-Boss.transform.right));
    }

    //An example attack coroutine, where the boss dashes across the screen
    [SerializeField] private float DashAttack_DashTime;
    [SerializeField] private float DashAttack_DashDistance;
    private IEnumerator DashAttack(Vector3 direction) {
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

    private IEnumerator Reposition() {
        yield return null;
    }
}
