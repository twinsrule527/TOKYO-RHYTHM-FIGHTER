using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Update() {
        //TODO add r to restart 
        
    }

    public static void PlayerWins() {

        //TODO call other peoples functions here, this is the high level one 
        Global.UIManager.PlayerWins();

    }

    public static void PlayerLoses() {

        //TODO call other peoples functions here, this is the high level one 
        Global.UIManager.PlayerLoses();

    }
}
