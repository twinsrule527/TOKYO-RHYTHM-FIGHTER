using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    [SerializeField] float perlinMult = 50f;
    [SerializeField] float shakeMagnitude = 1f;
    [SerializeField] float shakeTime = 1f;

    //if no vars specified, will shake with this thing's default values 
    public void ShakeMe() {

        StartCoroutine(ShakeTransform(transform, shakeMagnitude, shakeTime, perlinMult));
    }

    //if args specified, will shake according to input
    public void ShakeMe(float magnitude, float time) {
        
        StartCoroutine(ShakeTransform(transform, magnitude, time, perlinMult));
    }
    public void ShakeMe(float magnitude, float time, float perlinMultipl) {
        
        StartCoroutine(ShakeTransform(transform, magnitude, time, perlinMultipl));
    }

    //shake anything with this thing anywhere
    public static IEnumerator ShakeTransform(Transform t, float magnitude, float time, float perlinMultiplier) {

        Vector3 origPos = t.position;

        for(float tracker = 0; tracker < time; tracker += Time.deltaTime) {
            float thisMag = magnitude * (1 - (tracker / time)); //decreases mag over time 
            float x = Mathf.PerlinNoise(0, Time.time * perlinMultiplier) * thisMag - (thisMag / 2);
            float y = Mathf.PerlinNoise(Time.time * perlinMultiplier, 0) * thisMag - (thisMag / 2);
            Vector3 pos = new Vector3(origPos.x + x, origPos.y + y, origPos.z);
            t.position = pos;
            yield return null;
        }

        t.position = origPos;

    }
}
