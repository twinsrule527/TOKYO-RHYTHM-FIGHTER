using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceBar : MonoBehaviour
{

    [SerializeField] Sprite up, down;
    [SerializeField] SpriteRenderer myRenderer;
    [SerializeField] RectTransform textTransform;
    [SerializeField] float upPos, downPos;
    Vector3 upTransform, downTransform;

    // Start is called before the first frame update
    void Start()
    {
        //myRenderer = GetComponent<SpriteRenderer>();
        //textTransform = GetComponent<RectTransform>();
        upTransform = new Vector3(0, upPos, 0);
        downTransform = new Vector3(0, downPos, 0);
        textTransform.GetComponent<TextMeshPro>().sortingOrder = myRenderer.sortingOrder + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(BeatController.isPlaying) {
            if(BeatController.GetBeat() % 2 > 1) {
                myRenderer.sprite = up;
                textTransform.localPosition = upTransform;
            } else {
                myRenderer.sprite = down;
                textTransform.localPosition = downTransform;
            }
        }
    }
}
