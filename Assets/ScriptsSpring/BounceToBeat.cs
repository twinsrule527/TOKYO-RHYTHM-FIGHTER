using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceToBeat : MonoBehaviour
{
    //scale something's transform so it bounces to the beat!
    //TIP: utilize SQUASH AND STRETCH! 

    public float xScaleMax = 1;
    public float xScaleMin = 1;
    public float yScaleMax = 1;
    public float yScaleMin = 1;
    public float zScaleMin = 1;
    public float zScaleMax = 1;


    // Update is called once per frame
    void Update()
    {
        //bounce 2 the music 
        //scale according to BeatController.nearToBeat
        float vx = xScaleMin + ((xScaleMax - xScaleMin) * func(BeatController.beatOffset));
        float vy = yScaleMin + ((yScaleMax - yScaleMin) * func(BeatController.beatOffset));
        float vz = zScaleMin + ((zScaleMax - zScaleMin) * func(BeatController.beatOffset));
        transform.localScale = new Vector3(vx, vy, vz);
    }

    //|sin(x/2*pi)|
    //bouncy! 
    float func(float x) {
        return Mathf.Abs(Mathf.Sin((x) * Mathf.PI));
    }
}
