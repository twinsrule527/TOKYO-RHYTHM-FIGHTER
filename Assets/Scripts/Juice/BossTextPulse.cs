using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTextPulse : MonoBehaviour
{

    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //shake on beat 
        //via rotation

        float beat = BeatController.GetBeatInterp();
        //only shake in this window every beat 
        if(BeatController.IsOnBeat(1, beat)) {
                        //NOTE: will only work symmetrically if threshold before beat = thresh after beat
            float magnitude = BeatController.MINIMUM.thresholdBeforeBeat - BeatController.GetAbsDistanceFromBeat(1, beat);
            
        }

        //this was shake code 
/*
        Vector3 origPos = new Vector3(t.position.x, t.position.y, t.position.z);

        for(float tracker = 0; tracker < time; tracker += Time.deltaTime) {
            float thisMag = magnitude * (1 - (tracker / time)); //decreases mag over time 
            float x = Mathf.PerlinNoise(0, Time.time * perlinMultiplier) * thisMag - (thisMag / 2);
            float y = Mathf.PerlinNoise(Time.time * perlinMultiplier, 0) * thisMag - (thisMag / 2);
            Vector3 pos = new Vector3(origPos.x + x, origPos.y + y, origPos.z);
            t.position = pos;
            yield return null;
        }

        t.position = origPos;
*/
    }


}
