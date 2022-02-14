using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A simple example dash attack
public class PlaytestAttack2 : BossAttack
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private Transform myParent;
    [SerializeField] private float DashAttack_DashTime;
    [SerializeField] private float DashAttack_DashDistance;
    public override IEnumerator Attack() {
        Vector3 startPosition = myParent.position;
        Vector3 endPosition = myParent.position + direction * DashAttack_DashDistance;
        float curTime = 0;
        //At the start, it also starts an animation
        while(curTime < DashAttack_DashTime) {
            curTime += Time.deltaTime;
            myParent.transform.position = Vector3.Lerp(startPosition, endPosition, curTime / DashAttack_DashTime);
            yield return null;
        }
        transform.position = endPosition;
    }   

    public override void Interrupt(PlayerAction action) {

    }
    
    public override IEnumerator Cancel()
    {
        yield return null;
    }
    
    public override void CheckAttackSuccess()
    {
    }
}
