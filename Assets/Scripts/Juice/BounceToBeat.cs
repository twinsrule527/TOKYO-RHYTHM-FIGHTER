using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceToBeat : MonoBehaviour
{
    //scale something's transform so it bounces to the beat!
    //TIP: utilize SQUASH AND STRETCH! 

    [SerializeField] bool valuesAreDifferences = false;
    [SerializeField] bool invertBounce = false;
    public float beatFraction = 1f;

    public float xScaleMax = 1;
    public float xScaleMin = 1;
    public float yScaleMax = 1;
    public float yScaleMin = 1;
    public float zScaleMin = 1;
    public float zScaleMax = 1;

    float xdiff, ydiff, zdiff;

    void Start() {
        xdiff = xScaleMax - xScaleMin;
        ydiff = yScaleMax - yScaleMin;
        zdiff = zScaleMax - zScaleMin;

        if(valuesAreDifferences) {
            xScaleMax = transform.localScale.x + xScaleMax;
            xScaleMin = transform.localScale.x + xScaleMin;
            yScaleMax = transform.localScale.y + yScaleMax;
            yScaleMin = transform.localScale.y + yScaleMin;
            zScaleMax = transform.localScale.z + zScaleMax;
            zScaleMin = transform.localScale.z + zScaleMin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BeatController.isPlaying) {
            //bounce 2 the music 
            //scale according to BeatController.nearToBeat
            float val = func(BeatController.GetDistanceFromBeat(beatFraction)); //this crunches our distance from the last beat through the function below. range 0-1
            if(invertBounce) {
                val = invfunc(BeatController.GetDistanceFromBeat(beatFraction));
            }
            float vx = xScaleMin + (xdiff * val);
            float vy = yScaleMin + (ydiff * val);
            float vz = zScaleMin + (zdiff * val);
            transform.localScale = new Vector3(vx, vy, vz);
        }
        
    }

    //|sin(x/2*pi)|
    //bouncy! 
    /*
    float func(float x) {
        return Mathf.Abs(Mathf.Sin((x) * Mathf.PI));
    }
    */

    //|(x-1)^2| (standard)
    //snappy!
    float func(float x) {
        return Mathf.Pow(1.5f*x-1, 2);
    }    
    //|((x-1)^2*-1)+1| (inverted)
    //snappy!
    float invfunc(float x) {
        return (Mathf.Pow(1.5f*x-1, 2)*-1)+1;
    }
}
