using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{

    /*

        TODO: when we can and can't act (ex. waiting for start)
        or maybe they just cant move at the start lol 

        WISHLIST: controller support 

    */

    public bool DEBUG = true;

    public bool isPlayer1;

    public Character character;
    public PlayerSoundEffectController sfxController;
    public CharacterSpriteController spriteController;

    public Shaker charShake;

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

    float moveDistance = 1.5f;        //distance to move in space as one unit
    public int position = 0;        //how many units we are from starting point/baseline 
    public int posMax = 6;          //how many units you can move forward from baseline
    public int posMin = -2;         //how many units you can move back from baseline
    //int posCenter = 3;                  //how many units away from 0,0 each are

    // -2 -1 0 1 2 3 4 5 6 



    KeyCode forwardKey, backKey, highKey, lowKey; //attackKey;
    int moveSign = 1;


    public bool colliderHigh, colliderHighForward, colliderLow, colliderLowForward; 

    public bool actedThisBeat = false;

    public bool canControlCharacter = true;

    

    void Start() {

        int sign;
        if(isPlayer1) { sign = -1; }
        else { sign = 1; }

        //set position based on move distance 
        transform.position = new Vector3((moveDistance * posMax / 2 * sign), transform.position.y, transform.position.z);

        //posCenter = (int)(Mathf.Abs(transform.position.x) / moveDistance);

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

        if(Input.GetKeyDown(KeyCode.R)) {
            //restart R
            SceneManager.LoadScene("SampleScene");
        }

        if(!canControlCharacter) {
            return;
        }

        //only check key once
        bool forward = Input.GetKeyDown(forwardKey);
        bool back = Input.GetKeyDown(backKey);
        bool high = Input.GetKeyDown(highKey);
        bool low = Input.GetKeyDown(lowKey);
        //bool attack = Input.GetKeyDown(attackKey);

        
        //first of all, check if we're on beat. 
        //if we're on the beat, do all this logic. if we're not, mess up. 
        if((forward || back || high || low) && !actedThisBeat && !OLD_BeatController.IsOnBeat()) {

            //we're not on beat! 
            colliderHigh = false;
            colliderHighForward = false;
            colliderLow = false;
            colliderLowForward = false;
            sfxController.Sfx_MissBeat();
            spriteController.messUp();
            //charShake.screenshake(0.8f, 0.2f);
            charShake.screenshake(10, 10);
            character.messUp();
            actedThisBeat = true;

        } else if(!character.messedUp && !actedThisBeat) {
            
            if(forward && position < posMax) { //TODO check if the player can move and isnt blocked by the other player 
                
                //check if they're blocked by the other player! if they are, mess up. 
                if(character.otherInProximity()) {
                    sfxController.Sfx_CantForward();
                    charShake.screenshake(1, 0.2f);
                    actedThisBeat = true;
                } else {
                    //otherwise, they're free to move. 
                    position++;
                    transform.Translate(moveDistance * moveSign, 0, 0);
                    sfxController.Sfx_StepForward();
                    actedThisBeat = true;
                }
                
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
                    StartCoroutine(forwardReturn(true));

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
                    StartCoroutine(forwardReturn(false));
                    
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

        }

    }

    //change the sprite to forward but only for like half a second. it'll return to normal state after. 
    IEnumerator forwardReturn(bool isHigh) {

        yield return new WaitForSeconds(0.4f);
        
        /*
        if(isHigh && colliderHigh) {
            spriteController.high();
        } else if(colliderLow) {
            spriteController.low();
        }
        
        yield return new WaitForSeconds(0.1f);
        */

        spriteController.idle();
    }

}
