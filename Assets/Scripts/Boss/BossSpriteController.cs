using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpriteController : MonoBehaviour
{
    //Calls the animation for an attack
    public abstract void CallAttack(string ATKname, float ATKlength);

    //Called when the song actually starts, doesn't currently do anything
    public abstract void SongStarted();
}
