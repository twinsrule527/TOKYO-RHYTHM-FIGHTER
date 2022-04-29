using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private bool timer = false;
    public int time;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Global.TutorialManager.NextStage();
            timer = true;
        }



        if (timer == true)
        {

            time++;
            if (time > 100)
            {
                gameObject.SetActive(false);
            }


        }
    }
}
