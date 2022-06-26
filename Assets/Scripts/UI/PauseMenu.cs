using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] KeyCode [] pauseKeys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(GameManager.paused == false) {
            foreach(KeyCode key in pauseKeys) {
                if(Input.GetKeyDown(key)) {
                    GameManager.Pause();
                    Global.PauseScreen.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

}
