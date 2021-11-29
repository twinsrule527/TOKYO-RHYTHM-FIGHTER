using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    /*
        TODO 
        - multi inputs? make an array maybe 

        TO TEST:
        - attack key vs pressing a directional twice 
    */

    public bool DEBUG = true;

    public bool isPlayer1;

    public Character character;

    KeyCode p1_forward = KeyCode.D;
    KeyCode p1_back = KeyCode.A;
    KeyCode p1_high = KeyCode.W;
    KeyCode p1_low = KeyCode.S;
    KeyCode p1_attack;

    KeyCode p2_forward = KeyCode.LeftArrow;
    KeyCode p2_back = KeyCode.RightArrow;
    KeyCode p2_high = KeyCode.UpArrow;
    KeyCode p2_low = KeyCode.DownArrow;
    KeyCode p2_attack;

    float moveDistance = 1f;    //distance to move in space as one unit
    int position = 0;           //how many units we are from starting point/baseline 
    int posMax = 6;             //how many units you can move forward from baseline
    int posMin = -2;            //how many units you can move back from baseline
    int posCenter;              //how many units away from 0,0 each are. will be gotten in Start() 


    KeyCode forwardKey, backKey, highKey, lowKey, attackKey;
    int moveSign = 1;


    public GameObject debugcolliderHigh;
    public GameObject debugcolliderHighForward;
    public GameObject debugcolliderLow;
    public GameObject debugcolliderLowForward;


    bool colliderHigh, colliderHighForward, colliderLow, colliderLowForward; 


    public Sprite spr_High;
    public Sprite spr_HighForward;
    public Sprite spr_Low;
    public Sprite spr_LowForward;
    public Sprite spr_MessUp;

    public SpriteRenderer mySpriteRenderer;


    void Start() {

        posCenter = (int)(Mathf.Abs(transform.position.x));

        if(isPlayer1) {
            forwardKey = p1_forward;
            backKey = p1_back;
            highKey = p1_high;
            lowKey = p1_low;
            attackKey = p1_attack;

            //moveSign = 1;
        } else {
            forwardKey = p2_forward;
            backKey = p2_back;
            highKey = p2_high;
            lowKey = p2_low;
            attackKey = p2_attack;

            //moveSign = 1;
        }

    }


    // Update is called once per frame
    void Update() {

        //only check key once
        bool forward = Input.GetKeyDown(forwardKey);
        bool back = Input.GetKeyDown(backKey);
        bool high = Input.GetKeyDown(highKey);
        bool low = Input.GetKeyDown(lowKey);
        bool attack = Input.GetKeyDown(attackKey);

        //TODO first of all, check if we're on the beat. 
        //if we're on the beat, do all this logic. if we're not, mess up. 

        //first of all, check if we're on beat. 
        if(!BeatController.IsOnBeat()) {
            character.messUp();

        } else if(!character.messedUp) {
            
            if(forward && position < posMax) {
                position++;
                transform.Translate(moveDistance * moveSign, 0, 0);
            }
            if(back && position > posMin) {
                position--;
                transform.Translate(moveDistance * moveSign * -1, 0, 0);
            }


            if(high) {

                if(colliderHigh) {
                    //collider high is active. 
                    //set high forward to active. 
                    colliderHighForward= true;

                    mySpriteRenderer.sprite = spr_HighForward;
                    //TODO play sound 

                } else {
                    //collider high isn't active. 
                    //set collider high to active, set collider low to inactive, and set high forward to inactve
                    colliderHigh = true;
                    colliderHighForward = false;
                    colliderLow = false;

                    mySpriteRenderer.sprite = spr_High;
                    //TODO play sound 

                    //this could be the player blocking.
                    //TODO how to check blocking?
                    //check colliders? maybe there's a flag set on collide 
                    //alternatively, do this in the enter function? 
                }
                
            }
            if(low) {

                if(colliderLow) {
                    colliderLowForward = true;

                    mySpriteRenderer.sprite = spr_LowForward;
                    //TODO play sound 

                } else {
                    colliderLow = true;
                    colliderLowForward = false;
                    colliderHigh = false;

                    mySpriteRenderer.sprite = spr_Low;
                    //TODO play sound 

                    //TODO blocking 
                }
                
            }


            //toggle collider objects, which are really just for debugging now 
            if(DEBUG) {
                debugcolliderHigh.SetActive(colliderHigh);
                debugcolliderHighForward.SetActive(colliderHighForward);
                debugcolliderLow.SetActive(colliderLow);
                debugcolliderLowForward.SetActive(colliderLowForward);
            }


            //check collision and hits, with the bools. 


        }



        /*
        POSSIBLE STATES 
        high forward, high forward, forward meet in middle 
        high forward, high forward, cross 

        high, high forward- high blocks high forward, high gets a riposte 
        high, low forward- low gets a hit 
        low, high forward- high gets a hit 
        low, low forward- low blocks low forward, low gets a riposte 

        */

/*
        if(high) {

            if(colliderHigh.activeSelf) {
                //collider high is active. 
                //set high forward to active. 
                colliderHighForward.SetActive(true);

            } else {
                //collider high isn't active. 
                //set collider high to active, set collider low to inactive, and set high forward to inactve
                colliderHigh.SetActive(true);
                colliderHighForward.SetActive(false);
                colliderLow.SetActive(false);

                //this could be the player blocking.
                //TODO how to check blocking?
                //check colliders? maybe there's a flag set on collide 
                //alternatively, do this in the enter function? 
            }
            
        }
        if(low) {

            if(colliderLow.activeSelf) {
                colliderLowForward.SetActive(true);
            } else {
                colliderLow.SetActive(true);
                colliderLowForward.SetActive(false);
                colliderHigh.SetActive(false);

                //TODO blocking 
            }
            
        }
        */


    }

}
