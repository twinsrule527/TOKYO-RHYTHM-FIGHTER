using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A boss attack that waits until a given beat/waits a certain amt
public class WaitAttack : BossAttack
{
    [SerializeField] private float beatToWait;
    public override IEnumerator Attack() {
        yield return new WaitForSeconds(beatToWait);//Never do this
    }

    public override void Interrupt() {

    }
    
    public override IEnumerator Cancel()
    {
        yield return null;
    }
}
