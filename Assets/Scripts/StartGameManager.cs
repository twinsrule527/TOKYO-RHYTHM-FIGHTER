using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Manages the game start menu screen
public class StartGameManager : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    public void StartGame() {
        GameManager.ChangeScene(mainSceneName);
    }
}
