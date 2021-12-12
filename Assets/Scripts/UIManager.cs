using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Score text
    //[SerializeField]
    //public Text scoreText1;
    //public Text scoreText2;

    public Image p1Renderer;
    public Image p2Renderer;

    public Sprite [] p1Numbers;
    public Sprite [] p2Numbers;

    int p1score = 0;
    int p2score = 0;

    public Character p1;
    public Character p2;

    //p1.hitsScored
    //p1.points

    //p2.hitsScored
    //p2.points


    // Start is called before the first frame update
    void Start()
    {
        //scoreText1.text = " " + 0;
        //scoreText2.text = " " + 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(p1score != p1.hitsScored) {
            p1score = p1.hitsScored;
            p1Renderer.sprite = p1Numbers[p1score];
        }
        if(p2score != p2.hitsScored) {
            p2score = p2.hitsScored;
            p2Renderer.sprite = p2Numbers[p2score];
        }
        //scoreText1.text = " " + p1.hitsScored;
        //scoreText2.text = " " + p2.hitsScored;
    }

    IEnumerator scoreIncreaseGrow(Image renderer) {

        //TODO make the score number grow or something 

        yield return null;
    }

}
