using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboIndicator : MonoBehaviour
{
    
    public TextMeshProUGUI comboText;
    [SerializeField] private string TextBeforeComboNum;
    [SerializeField] private string TextAfterComboNum;

    public static int comboCounter = 0;
    private static int maxComboCountDmg = 20;//How much the combo Multiplier can apply to attacks, at most
    private List<PlayerAction> playerActions;

    private static float dmgMultiplier = 0.1f;

    void Start()
    {
        playerActions = new List<PlayerAction>(FindObjectsOfType<PlayerAction>());
        
        ComboIndicator.comboCounter = 0;
    }
    void Update()
    {
        comboText.text = TextBeforeComboNum + comboCounter.ToString() + TextAfterComboNum;

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

    public static float comboMultiplier(float inputAmt) {
        int comboAmt = Mathf.Min(comboCounter, maxComboCountDmg);
        inputAmt = inputAmt + inputAmt*(comboAmt * dmgMultiplier);
        
        return inputAmt;

    }
}
