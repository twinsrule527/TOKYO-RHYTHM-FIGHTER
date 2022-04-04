using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitsPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake.ShakeTransform(Camera.main.transform, 10, 0.2f, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
