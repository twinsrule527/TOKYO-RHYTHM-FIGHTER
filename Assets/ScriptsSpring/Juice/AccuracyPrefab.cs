using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyPrefab : MonoBehaviour
{

    [SerializeField] SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called by the PlayerSpriteController when this accuracy needs to be displayed.
    public void DisplayAccuracy() {

        StopCoroutine(DisplayCoroutine()); //if this accuracy was already being displayed
        StartCoroutine(DisplayCoroutine());
        
    }

    IEnumerator DisplayCoroutine() {
        mySpriteRenderer.enabled = true;

        //TODO do things here 
        //example: 
        /*
        for(float time = 0; time < 1f, time += Time.deltaTime) {
            //change the opacity,size,whatever relative to time (or another var but accounting for deltaTime)
            yield return null; //this means, wait til the next frame to continue. 
                                // so in this case, this for loop loops once per frame til it's done.
        }
        */
        yield return new WaitForSeconds(0.5f);

        mySpriteRenderer.enabled = false;

    }
}
