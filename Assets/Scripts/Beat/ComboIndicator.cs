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

    public float dmgMultiplier = 0.2f;

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

        foreach (PlayerAction action in playerActions )
        {
            if(action.IsComboable) {
                action.damage = comboMultiplier(action.baseDamage);// * ((comboCounter + 1) * 1.2f);
                //action.damage = action.baseDamage;

                if(comboCounter == 0){
                    action.damage = action.baseDamage;
                }
            }
        }
    }

    private float comboMultiplier(float inputAmt) {
        
        inputAmt = inputAmt + inputAmt*(comboCounter * dmgMultiplier);
        
        return inputAmt;

    }
}
