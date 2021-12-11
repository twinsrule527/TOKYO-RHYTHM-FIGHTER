using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSplash : MonoBehaviour
{

    public SpriteRenderer block_high;
    public SpriteRenderer block_low;
    public SpriteRenderer clash; 

/*
    public SpriteRenderer high_OK;
    public SpriteRenderer high_GOOD;
    public SpriteRenderer high_GREAT;
    public SpriteRenderer high_PERFECT;

    public SpriteRenderer low_OK;
    public SpriteRenderer low_GOOD;
    public SpriteRenderer low_GREAT;
    public SpriteRenderer low_PERFECT;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showClash() {
        StartCoroutine(showSplash(clash));
    }
    public void showBlockHigh() {
        StartCoroutine(showSplash(block_high));
    }
    public void showBlockLow() {
        StartCoroutine(showSplash(block_low));
    }

    IEnumerator showSplash(SpriteRenderer renderer) {

        renderer.gameObject.SetActive(true);

        renderer.color = Color.white;

        float growTime = 0.4f;
        float fadeTime = 0.5f;

        Vector3 scale = renderer.gameObject.transform.localScale;
        Vector3 origScale = new Vector3(scale.x, scale.y, scale.z);

        //grow fast 
        for(float t = 0.2f; t / growTime < growTime; t += Time.deltaTime) {
            renderer.gameObject.transform.localScale = new Vector3(origScale.x * t / growTime, origScale.y * t / growTime, origScale.z);
            yield return null;
        }
        renderer.gameObject.transform.localScale = origScale;

        //fade out
        for(float t = 0; t / fadeTime < fadeTime; t += Time.deltaTime) {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (fadeTime - t) / fadeTime);
            renderer.gameObject.transform.localScale = new Vector3(origScale.x + t, origScale.y + t, origScale.z);
            yield return null;
        }
        renderer.gameObject.transform.localScale = origScale;

        renderer.gameObject.SetActive(false);
        
    }

    //public void showSplash(bool wasHigh, BeatController.Accuracy accuracy) {

        //generate a little random position and angle (??? or make each one a little different??)

        //grow 
        //fade out 
        //use coroutines with deltatime probably 

    //}

}
