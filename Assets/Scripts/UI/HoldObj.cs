using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObj : MonoBehaviour
{

    //int waitForStartup = 10;
    // Start is called before the first frame update
    int counter = 1;
    void Awake()
    {
        transform.rotation = Quaternion.identity;
        //transform.rotation = GetComponentInParent<Transform>().rotation;
        //transform.position = 1.14f;
    }

    void Start() {
        gameObject.SetActive(false);
    }
    
    void Update() {
        //if(waitForStartup > 0) {
        //    waitForStartup--;
        //}
    }

    void OnEnable()
    {
        if(counter > 0) {
            counter--;
        } else {
            gameObject.GetComponent<Shake>().ShakeIt(2f, 0.2f, 50f);
        }
    }

}
