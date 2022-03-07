using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Manages the game start menu screen
public class StartGameManager : MonoBehaviour
{
    [SerializeField] private int mainSceneNum;
    public void StartGame() {
        SceneManager.LoadScene(mainSceneNum);
    }
}
