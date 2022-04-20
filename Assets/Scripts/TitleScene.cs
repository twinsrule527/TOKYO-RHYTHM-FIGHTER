using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{

    [SerializeField] GameObject enableIfSeenCutscene;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.hasSeenOpeningCutscene) {
            enableIfSeenCutscene.SetActive(true);
        }
    }

}
