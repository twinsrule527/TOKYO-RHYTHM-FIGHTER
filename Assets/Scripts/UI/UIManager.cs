using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//A general UI Manager
    //As of sprint 1, only displays player and enemy health
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerHealthText;
    [SerializeField] private TMP_Text EnemyHealthText;

    void Start() {
        Global.UIManager = this;
    }

    public void SongStarted() {
        
    }

    //Sets the text to player and enemy's current health - called by change Health functions
    /*public void SetHealthText() {
        PlayerHealthText.text = "Player Health: " + ((int)Global.Player.playerHealth).ToString();
        EnemyHealthText.text = "Boss Health: " + ((int)Global.Boss.bossHP).ToString();
    }*/
/*
    public void RestartButtonPressed() {
        Debug.Log("restart button pressed");
        GameManager.RestartScene();
    }

    public void ExitButtonPressed() {
        Debug.Log("exit button pressed");
        GameManager.GoToTitle();
    }
*/
    public void PlayerWins() {
        //TODO show sprite 
    }

    public void PlayerLoses() {
        //TODO show sprite
    }

    public void Pause() {
        //TODO
    }

    public void UnPause() {
        //TODO
    }

    //TODO if window not focused and/or mouse leaves window, pause the game 
}
