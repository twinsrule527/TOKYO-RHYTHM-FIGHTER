using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInput : MonoBehaviour
{

    /*
        Class for all input that's not during gameplay/attacks 
        Like, using the UI, or pressing R to restart, or pressing continue on dialogue/cutscenes.
        This class will call functions on other things to actually do stuff. 
        This just manages keyboard+mouse input. 
    */

    KeyCode key_restart = KeyCode.R;

    //KeyCode key_startsong = KeyCode.Return;

/*
    KeyCode [] key_select = {KeyCode.E, KeyCode.Space, KeyCode.Return};
    KeyCode [] key_back = {KeyCode.Escape, KeyCode.Backspace};

    KeyCode [] key_left = {KeyCode.A, KeyCode.LeftArrow};
    KeyCode [] key_right = {KeyCode.D, KeyCode.RightArrow};
    KeyCode [] key_up = {KeyCode.W, KeyCode.UpArrow};
    KeyCode [] key_down = {KeyCode.S, KeyCode.DownArrow};
*/

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(key_restart)) {
            Restart();
        }
/*
        if(Input.GetKeyDown(key_startsong)) {
            GameManager.StartSong();
        }

        foreach(KeyCode kc in key_select) {
            if(Input.GetKeyDown(kc)) {
                Select();
            }
        }

        foreach(KeyCode kc in key_back) {
            if(Input.GetKeyDown(kc)) {
                Back();
            }
        }

        foreach(KeyCode kc in key_left) {
            if(Input.GetKeyDown(kc)) {
                Left();
            }
        }

        foreach(KeyCode kc in key_right) {
            if(Input.GetKeyDown(kc)) {
                Right();
            }
        }

        foreach(KeyCode kc in key_up) {
            if(Input.GetKeyDown(kc)) {
                Up();
            }
        }

        foreach(KeyCode kc in key_down) {
            if(Input.GetKeyDown(kc)) {
                Down();
            }
        }
*/
    }

    void Restart() {
        GameManager.RestartScene();
    }

    /*

    void Select() {
        //TODO what to do when we press select?
    }

    void Back() {
        //TODO what to do when we press back?
    }

    void Left() {

    }

    void Right() {

    }

    void Up() {

    }

    void Down() {

    } */
}
