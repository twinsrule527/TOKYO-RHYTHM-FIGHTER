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

    public static bool gameplayRunning = false;

    public static int currentStage; //The current stage that the boss is in


    void Awake() {
        Global.FailScreen = GameObject.FindWithTag("FailScreenObj");
        if(Global.FailScreen != null) {
            Global.FailScreen.SetActive(false);
        } else if(SceneManager.GetActiveScene().buildIndex == 3){
            Debug.Log("ERR: no FailScreen found-- make sure it's ENABLED in the scene");
        }
        Global.PauseScreen = GameObject.FindWithTag("PauseScreenObj");
        if(Global.PauseScreen != null) {
            Global.PauseScreen.SetActive(false);
        } else if(SceneManager.GetActiveScene().buildIndex >= 2){
            Debug.Log("ERR: no PauseMenu found-- make sure it's ENABLED in the scene");
        }
        SfxSync.soundEffectsEnabled = true;
    }


    //start playing the song. 
    //this one with no input uses the one given to the beat controller.
    public static void StartSong() {
        BeatController.StartSong();
        gameplayRunning = true;
    }
    //this one uses whatever input you give it.
    public static void StartSong(SongData songData) {
        BeatController.StartSong(songData);
        gameplayRunning = true;
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
        GoToTutorial();
    }

    public static void GoToTutorial() {
        SceneManager.LoadScene(2);
    }

    public static void GoToGame() {
        SceneManager.LoadScene(3);
    }

    public static void GoToWin() {
        SceneManager.LoadScene(4);
    }

    public static void QuitGame() {
        //todo: do we want to save anything? like if player has viewed intro cutscene 
        Application.Quit();
    }

    //restart the current scene 
    public static void RestartScene() {
        BeatController.UnPauseOutsideMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //change to a specific scene 
    public static void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    //this is the high level function called when the player wins
    //it shouldn't really do any logic here, it should call other things' highlevel functions
    public static void PlayerWins() {

        Global.UIManager.PlayerWins();

        BeatController.WinStop();

        SfxSync.soundEffectsEnabled = false;

    }

    //called after the game has slowed to a stop. 
    public static void PlayerWinsFinish() {

            SfxSync.soundEffectsEnabled = true;

            GameManager.GoToWin();
    }

    //this is the high level function called when the player loses 
    //it shouldn't really do any logic here, it should call other things' highlevel functions
    public static void PlayerLoses() {

        if(gameplayRunning) {

            gameplayRunning = false;

            BeatController.FailStop();

            Global.UIManager.PlayerLoses();

            SfxSync.soundEffectsEnabled = false;

        }
    }

    //called after the game has slowed to a stop. 
    public static void PlayerLosesFinish() {

        SfxSync.soundEffectsEnabled = true;

        if(Global.FailScreen != null) {
            Global.FailScreen.SetActive(true);
        } else {
            Debug.Log("ERROR: couldn't enable fail screen because reference was null!");
        }
    }

    public static void Pause() {
        //AudioListener.pause = true;
        BeatController.PauseByMenu();
        gameplayRunning = false;
        Global.PauseScreen.SetActive(true);
        Global.UIManager.Pause();
        //todo pause coroutines
        //todo pause time?
    }

    public static void UnPause() {
        Global.PauseScreen.SetActive(false);
        Global.UIManager.UnPause();
        gameplayRunning = true;
        BeatController.UnPauseByMenu();
        //todo unpause coroutines 
        //todo pause time? 
        //AudioListener.pause = false;
    }
  
}
