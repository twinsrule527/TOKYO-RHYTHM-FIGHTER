using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) ) {
            Global.TutorialManager.NextStage();
            gameObject.SetActive(false);
        }
    }
}
