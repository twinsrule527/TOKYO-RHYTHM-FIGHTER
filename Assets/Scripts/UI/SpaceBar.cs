using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBar : MonoBehaviour
{

    [SerializeField] Sprite up, down;
    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BeatController.isPlaying) {
            if(BeatController.GetBeat() % 2 > 1) {
                myRenderer.sprite = up;
            } else {
                myRenderer.sprite = down;
            }
        }
    }
}
