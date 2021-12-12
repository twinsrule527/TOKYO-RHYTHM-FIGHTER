using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    float perlinMult = 50f;

    public IEnumerator screenshake(float magnitude, float time) {
        
        Vector3 origPos = transform.position;

        for(float tracker = 0; tracker < time; tracker += Time.deltaTime) {
            float thisMag = magnitude * (1 - (tracker / time)); //decreases mag over time 
            float x = Mathf.PerlinNoise(0, Time.time * perlinMult) * thisMag - (thisMag / 2);
            float y = Mathf.PerlinNoise(Time.time * perlinMult, 0) * thisMag - (thisMag / 2);
            Vector3 pos = new Vector3(origPos.x + x, origPos.y + y, origPos.z);
            transform.position = pos;
            yield return null;
        }

        //for players- if they havent moved- 
        //if(Mathf.Abs(transform.position.y - origPos.y) < magnitude) {
        transform.position = origPos;
        //}
        
    }
}
