using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    //THE TOTAL HIGHEST FOR ALL
    //NEEDS TO BE UPDATED IF ADDING MORE BEATS 
    static float total = 3f;

    int sign;
    float initial;

    // Start is called before the first frame update
    void Start()
    {
        initial = transform.localPosition.x;
        sign = (int)Mathf.Sign(initial);
    }

    // Update is called once per frame
    void Update()
    {
        float newx = BeatController.beat % total + total;
        newx = Mathf.Repeat(newx + initial, total);
        newx *= sign;
        newx = newx + (total * -sign);
        transform.localPosition = new Vector3(newx, transform.localPosition.y, transform.localPosition.z);
    }
}
