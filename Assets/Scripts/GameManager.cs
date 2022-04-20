using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    /*
        This class is for high level game state management only 
        changing high level gamestate like restarting the scene, changing the scene, and
        calling changes when the player wins and loses 
        little logic should be in here, it should call other scripts' functions. 
    */

    public static bool hasSeenOpeningCutscene = false;

    static GameObject failScreenObj;

    void Start() {
        failScreenObj = GameObject.FindWithTag("FailScreenObj");
        if(failScreenObj != null) {
            failScreenObj.SetActive(false);
        }
    }

    //start playing the song. 
    //this one with no input uses the one given to the beat controller.
    public static void StartSong() {
        BeatController.StartSong();
    }
    //this one uses whatever input you give it.
    public static void StartSong(SongData songData) {
        BeatController.StartSong(songData);
    }

    //the song has started playing- call everybody who needs to know!
    public static void SongStarted(SongData songData) {

        if(Global.BeatIndicatorBrain == null) { //but don't do anything if we're not in the game scene
            return;
        }  
        
        Global.BeatIndicatorBrain.enabled = true;
        Global.Player.enabled = true;
        
        Global.BeatIndicatorBrain.SongStarted(); //boss attacks depend on brain, call first 
        Global.Player.SongStarted();
        Global.Boss.SongStarted();
        Global.UIManager.SongStarted();
    }

    public static void GoToTitle() {
        SceneManager.LoadScene(0);
    }

    public static void GoToIntroCutscene() {
        SceneManager.LoadScene(1);
    }

    public static void StartFromIntroCutscene() {
        hasSeenOpeningCutscene = true;
        GoToGame();
    }

    public static void GoToGame() {
        SceneManager.LoadScene(2);
    }

    public static void GoToWin() {
        SceneManager.LoadScene(3);
    }

    //restart the current scene 
    public static void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //change to a specific scene 
    public static void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    //this is the high level function called when the player wins
    //it shouldn't really do any logic here, it should call other things' highlevel functions
    public static void PlayerWins() {

        //TODO call other things' functions here, this is the high level one 
        Global.UIManager.PlayerWins();

    }

    //this is the high level function called when the player loses 
    //it shouldn't really do any logic here, it should call other things' highlevel functions
    public static void PlayerLoses() {

        //TODO call other things' functions here, this is the high level one 
        Global.UIManager.PlayerLoses();

        //show the fail ui 
        failScreenObj.SetActive(true);

    }
  
}
