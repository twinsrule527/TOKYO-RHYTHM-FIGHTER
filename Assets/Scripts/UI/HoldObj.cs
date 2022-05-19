using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
        //transform.rotation = GetComponentInParent<Transform>().rotation;
    }

    void OnEnable()
    {
        gameObject.GetComponent<Shake>().ShakeIt(2f, 0.2f, 50f);
    }

}
