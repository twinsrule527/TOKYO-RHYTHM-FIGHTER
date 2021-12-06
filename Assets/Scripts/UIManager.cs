using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Score text
    [SerializeField]
    public Text scoreText1;
    public Text scoreText2;

    public Character p1;
    public Character p2;

    //p1.hitsScored
    //p1.points

    //p2.hitsScored
    //p2.points


    // Start is called before the first frame update
    void Start()
    {
        scoreText1.text = "Player 1 Hits: " + 0;
        scoreText2.text = "Player 2 Hits: " + 0;
    }

    public void AddScore()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText1.text = "Player 1 Hits: " + p1.hitsScored;
        scoreText2.text = "Player 2 Hits: " + p2.hitsScored;
    }
}
