using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCamScript : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(cam.aspect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
