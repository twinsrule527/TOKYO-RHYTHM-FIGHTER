using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    bool gameEndsPointThreshold = true;


    public Character otherPlayer;



    static int pointsToGame = 3;
    public int points = 0;

    public int hitsScored = 0;



    //// POINT VALUES ////
    int pval_block = 1;
    int pval_hit = 1;

    float pmult_OK = 0.5f;
    float pmult_GOOD = 0.75f;
    float pmult_GREAT = 1f;
    float pmult_PERFECT = 1.25f;


    bool iHit = false;
    public bool wasBlocked = false;
    public bool messedUp = false;



    public SpriteSplash spriteSplashController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player tries to take an action off the beat. 
    public void messUp() {

        messedUp = true;

        //TODO play a sound 

        //TODO splash
    }

    //block the other's attack. 
    //initiates a riposte. TODO 
    public void block(bool wasHigh, BeatController.Accuracy accuracy) {

        otherPlayer.wasBlocked = true; 

        //TODO display a splash! 

    }

    //high/low forward collides with body. 
    //this could get overwritten by a block, so it won't score until the end of the beat threshold. 
    public void hit(bool wasHigh, BeatController.Accuracy accuracy) {

        iHit = true;

        //display a splash! 
        spriteSplashController.showSplash(wasHigh, accuracy);
    }

    //called every time the beat threshold is over. 
    //adds hits scored, resets variables. 
    public void endOfBeat() {

        //tally up any hits that weren't blocked this beat. 
        if(iHit && !wasBlocked) {
            //i hit and wasn't blocked. score a point 
            hitsScored++;
            if(gameEndsPointThreshold && hitsScored == pointsToGame) {
                //WE HAVE A WINNER 
                wonTheGame();
            }
        }

        //reset variables and flags. 
        iHit = false;
        wasBlocked = false;
        messedUp = false;

    }


    void wonTheGame() {
        //TODO 
    }




}
