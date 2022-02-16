using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    [SerializeField] static float total;

    int sign;

    // Start is called before the first frame update
    void Start()
    {
        sign = (int)Mathf.Sign(transform.localPosition.x);
    }

    // Update is called once per frame
    void Update()
    {
        float newx = Mathf.Abs(transform.localPosition.x);
        newx += BeatController.beat;
        newx = sign * Mathf.Repeat(newx, total);
        transform.localPosition = new Vector3(newx, transform.localPosition.y, transform.localPosition.z);
    }
}
