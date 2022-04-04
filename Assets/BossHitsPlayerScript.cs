using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitsPlayerScript : MonoBehaviour
{

    void OnEnable() {
        Debug.Log("tried to shake camera");
        Camera.main.gameObject.GetComponent<Shake>().ShakeIt(2f, 0.25f, 50f);
        //StartCoroutine(Shake.ShakeTransform(Camera.main.transform, 10, 0.2f, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
