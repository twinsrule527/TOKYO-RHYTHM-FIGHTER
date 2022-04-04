using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitsPlayerScript : MonoBehaviour
{

    void OnEnable() {
        Camera.main.gameObject.GetComponent<Shake>().ShakeIt(1.5f, 0.2f, 50f);
        //StartCoroutine(Shake.ShakeTransform(Camera.main.transform, 10, 0.2f, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
