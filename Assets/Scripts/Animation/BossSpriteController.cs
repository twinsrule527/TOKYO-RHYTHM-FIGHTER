using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpriteController : MonoBehaviour
{
    public abstract void CallAttack(string ATKname, float ATKlength);
}
