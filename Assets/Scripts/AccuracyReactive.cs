using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyReactive : MonoBehaviour
{

    public bool ok, good, great, perfect;
    public float offOpacity = 0;
    public float onOpacity = 1;

    Color off, on;

    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        Color c = myRenderer.color;
        off = new Color(c.r, c.g, c.b, offOpacity);
        on = new Color(c.r, c.g, c.b, onOpacity);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(ok) {
            if(BeatController.accuracy == BeatController.Accuracy.OK) {
                myRenderer.color = on;
                return;
            } else {
                myRenderer.color = off;
            }
        }
        if(good) {
            if(BeatController.accuracy == BeatController.Accuracy.GOOD) {
                myRenderer.color = on;
                return;
            } else {
                myRenderer.color = off;
            }
        }
        if(great) {
            if(BeatController.accuracy == BeatController.Accuracy.GREAT) {
                myRenderer.color = on;
                return;
            } else {
                myRenderer.color = off;
            }
        }
        if(perfect) {
            if(BeatController.accuracy == BeatController.Accuracy.PERFECT) {
                myRenderer.color = on;
                return;
            } else {
                myRenderer.color = off;
            }
        }
    }
}
