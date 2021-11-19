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

    public bool isPlayer1;

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


    KeyCode forwardKey, backKey, highKey, lowKey, attackKey;
    int moveSign;



    public GameObject colliderHigh;
    public GameObject colliderHighForward;
    public GameObject colliderLow;
    public GameObject colliderLowForward;



    void Start() {

        if(isPlayer1) {
            forwardKey = p1_forward;
            backKey = p1_back;
            highKey = p1_high;
            lowKey = p1_low;
            attackKey = p1_attack;

            moveSign = 1;
        } else {
            forwardKey = p2_forward;
            backKey = p2_back;
            highKey = p2_high;
            lowKey = p2_low;
            attackKey = p2_attack;

            moveSign = -1;
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

        if(forward && position < posMax) {
            position++;
            transform.Translate(moveDistance * moveSign, 0, 0);
        }
        if(back && position > posMin) {
            position--;
            transform.Translate(moveDistance * moveSign * -1, 0, 0);
        }


        /*
            TODO 
            logic for enabling/disabling forward colliders, blocking,
        */
        if(high) {
            colliderHigh.SetActive(true);
            colliderLow.SetActive(false);
        }
        if(low) {
            colliderLow.SetActive(true);
            colliderHigh.SetActive(false);
        }


    }
}
