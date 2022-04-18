using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboIndicator : MonoBehaviour
{
    
    public TextMeshProUGUI comboText;

    public static int comboCounter = 0;
    private List<PlayerAction> playerActions;

    void Start()
    {
        playerActions = new List<PlayerAction>(FindObjectsOfType<PlayerAction>());
    }
    void Update()
    {
        comboText.text = comboCounter.ToString() + " HITS!";

        if(comboCounter == 0){
            comboText.enabled = false;
        } else{
            comboText.enabled = true;
        }
    }
}
