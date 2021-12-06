using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    /*

        WISHLIST: controller support 

    */

    public bool DEBUG = true;

    public bool isPlayer1;

    public Character character;
    public PlayerSoundEffectController sfxController;
    public CharacterSpriteController spriteController;

    KeyCode p1_forward = KeyCode.D;
    KeyCode p1_back = KeyCode.A;
    KeyCode p1_high = KeyCode.W;
    KeyCode p1_low = KeyCode.S;
    //KeyCode p1_attack;

    KeyCode p2_forward = KeyCode.LeftArrow;
    KeyCode p2_back = KeyCode.RightArrow;
    KeyCode p2_high = KeyCode.UpArrow;
    KeyCode p2_low = KeyCode.DownArrow;
    //KeyCode p2_attack;

    float moveDistance = 1f;        //distance to move in space as one unit
    public int position = 0;        //how many units we are from starting point/baseline 
    public int posMax = 6;          //how many units you can move forward from baseline
    public int posMin = -2;         //how many units you can move back from baseline
    int posCenter;                  //how many units away from 0,0 each are. will be gotten in Start() 


    KeyCode forwardKey, backKey, highKey, lowKey; //attackKey;
    int moveSign = 1;


    public GameObject debugcolliderHigh;
    public GameObject debugcolliderHighForward;
    public GameObject debugcolliderLow;
    public GameObject debugcolliderLowForward;


    public bool colliderHigh, colliderHighForward, colliderLow, colliderLowForward; 

    public bool actedThisBeat = false;


    

    void Start() {

        posCenter = (int)(Mathf.Abs(transform.position.x));

        if(isPlayer1) {
            forwardKey = p1_forward;
            backKey = p1_back;
            highKey = p1_high;
            lowKey = p1_low;
            //attackKey = p1_attack;

            //moveSign = 1;
        } else {
            forwardKey = p2_forward;
            backKey = p2_back;
            highKey = p2_high;
            lowKey = p2_low;
            //attackKey = p2_attack;

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
        //bool attack = Input.GetKeyDown(attackKey);

        
        //first of all, check if we're on beat. 
        //if we're on the beat, do all this logic. if we're not, mess up. 
        if((forward || back || high || low) && !actedThisBeat && !BeatController.IsOnBeat()) {

            //we're not on beat! 
            colliderHigh = false;
            colliderHighForward = false;
            colliderLow = false;
            colliderLowForward = false;
            sfxController.Sfx_MissBeat();
            spriteController.messUp();
            character.messUp();
            actedThisBeat = true;

        } else if(!character.messedUp && !actedThisBeat) {
            
            if(forward && position < posMax) { //TODO check if the player can move and isnt blocked by the other player 
                position++;
                transform.Translate(moveDistance * moveSign, 0, 0);
                sfxController.Sfx_StepForward();
                actedThisBeat = true;
            }
            if(back && position > posMin) {
                position--;
                transform.Translate(moveDistance * moveSign * -1, 0, 0);
                sfxController.Sfx_StepBack();
                actedThisBeat = true;
            }


            if(high) {
                actedThisBeat = true;

                if(colliderHigh) {
                    //collider high is active. 
                    //set high forward to active. 
                    colliderHighForward= true;

                    spriteController.highForward();

                    sfxController.Sfx_HighForward();

                    character.checkHit();

                } else {
                    //collider high isn't active. 
                    //set collider high to active, set collider low to inactive, and set high forward to inactve
                    colliderHigh = true;
                    colliderHighForward = false;
                    colliderLow = false;

                    spriteController.high();

                    sfxController.Sfx_High();

                    //this could be the player blocking. check if we blocked the other player.
                    character.checkBlock();
                    
                }
                
            }
            if(low) {
                actedThisBeat = true;

                if(colliderLow) {
                    colliderLowForward = true;

                    spriteController.lowForward();
                    
                    sfxController.Sfx_LowForward();

                    character.checkHit();

                } else {
                    colliderLow = true;
                    colliderLowForward = false;
                    colliderHigh = false;

                    spriteController.low();
                    
                    sfxController.Sfx_Low();

                    character.checkBlock();

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
