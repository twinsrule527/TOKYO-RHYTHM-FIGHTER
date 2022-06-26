using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public IEnumerator currentCoroutine;
    public AnimationController currentAnimation;//Attached to currentCoroutine
    public Vector3 basePosition {protected set; get;}
}
