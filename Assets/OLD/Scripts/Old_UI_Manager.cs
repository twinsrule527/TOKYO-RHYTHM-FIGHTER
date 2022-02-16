using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Old_UI_Manager : MonoBehaviour
{

    //Score text
    //[SerializeField]
    //public Text scoreText1;
    //public Text scoreText2;

    public Image p1Renderer;
    public Image p2Renderer;
    public Image p1juice;
    public Image p2juice;

    public float startScale;
    float endScale;
    public float startOpacity;
    public float endOpacity;

    public float timescale;

    public Sprite [] p1Numbers;
    public Sprite [] p2Numbers;

    int p1score = 0;
    int p2score = 0;

    public Character p1;
    public Character p2;

    Vector3 startScaleV, endScaleV;
    Color startC, endC;

    //p1.hitsScored
    //p1.points

    //p2.hitsScored
    //p2.points


    // Start is called before the first frame update
    void Start()
    {
        //scoreText1.text = " " + 0;
        //scoreText2.text = " " + 0;

        endScale = p1Renderer.transform.localScale.x;

        startScaleV = new Vector3(startScale, startScale, 1);
        endScaleV = new Vector3(endScale, endScale, 1);

        startC = new Color(1, 1, 1, startOpacity);
        endC = new Color(1, 1, 1, endOpacity);
    }

    // Update is called once per frame
    void Update()
    {

        if(p1score != p1.hitsScored) {
            p1score = p1.hitsScored;
            p1Renderer.sprite = p1Numbers[p1score];
            //p1juice.sprite = p1Numbers[p1score];
            //StartCoroutine(scoreIncreaseJuice(p1juice));
        }
        if(p2score != p2.hitsScored) {
            p2score = p2.hitsScored;
            p2Renderer.sprite = p2Numbers[p2score];
            //p2juice.sprite = p2Numbers[p2score];
            //StartCoroutine(scoreIncreaseJuice(p2juice));
        }
        //scoreText1.text = " " + p1.hitsScored;
        //scoreText2.text = " " + p2.hitsScored;
    }

    //why doesnt this look right :-( 
    IEnumerator scoreIncreaseJuice(Image renderer) {

        renderer.gameObject.SetActive(true);

        renderer.transform.localScale = startScaleV;
        renderer.color = startC;

        for(float tracker = 0; renderer.transform.localScale.x - 0.01f > endScale; tracker += Time.deltaTime * timescale) {
            renderer.transform.localScale = Vector3.Lerp(startScaleV, endScaleV, tracker);
            renderer.color = Color.Lerp(startC, endC, tracker);
            yield return null;
        }
        renderer.gameObject.SetActive(false);
        
    }

    //IEnumerator juiceScore(MusicReactive bouncing) {
        //TODO just make it bounce bigger for a sec lol 
    //}

}
