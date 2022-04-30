using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ComboIndicator : MonoBehaviour
{
    
    public TextMeshProUGUI comboText;
    [SerializeField] private string TextBeforeComboNum;
    [SerializeField] private string TextAfterComboNum;

    public static int comboCounter = 0;
    private int prevComboCounter;
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

        if(comboCounter == prevComboCounter) {
            return;
        }

        prevComboCounter = comboCounter;

        StringBuilder builder = new StringBuilder();
        builder.Append(TextBeforeComboNum);
        builder.Append(comboCounter);
        builder.Append(TextAfterComboNum);
        builder.Append('!', comboCounter - 1);
        comboText.text = builder.ToString();

        if(comboCounter >= 2){
            comboText.enabled = true;
        } else{
            comboText.enabled = false;
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
