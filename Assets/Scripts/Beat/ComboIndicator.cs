using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ComboIndicator : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI [] comboTexts;
    [SerializeField] private string TextBeforeComboNum;
    [SerializeField] private string TextAfterComboNum;

    private static int comboCounter = 0;
    private static int maxComboCounted = 20; //past this number of notes combo'd, won't matter
    private List<PlayerAction> playerActions;

    public static readonly float dmgMultiplier = 0.2f; //actually the amount of damage added with each note in a combo- TODO rename this 

    [SerializeField] ShakeRect shaker;

    void Awake() {
        Global.ComboIndicator = this;

        playerActions = new List<PlayerAction>(FindObjectsOfType<PlayerAction>());
        
        SetCombo(0);
    }

    public int GetCombo() {
        return comboCounter;
    }

    public void IncrementCombo(int numChangeBy = 1) {
        SetCombo(comboCounter + numChangeBy);
    }

    public void SetCombo(int numSetTo) {

        comboCounter = numSetTo;

        StringBuilder builder = new StringBuilder();
        builder.Append(TextBeforeComboNum);
        builder.Append(comboCounter);
        builder.Append(TextAfterComboNum);
        if(comboCounter > 1) {
            builder.Append('!', comboCounter - 1);
        }

        foreach(TextMeshProUGUI comboText in comboTexts) {
            
            comboText.text = builder.ToString();

            if(comboCounter >= 2){
                comboText.enabled = true;
                shaker.ShakeIt();
            } else{
                comboText.enabled = false;
            }
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
        int comboAmt = Mathf.Min(comboCounter - 1, maxComboCounted); //-1 because a single attack is "1" 
        inputAmt = inputAmt + (comboAmt * dmgMultiplier);
        
        return inputAmt;

    }

    bool enabledBeforePause = false;
    public void Pause() {
        enabledBeforePause = comboTexts[0].enabled;
        foreach(TextMeshProUGUI comboText in comboTexts) {
            comboText.enabled = false;
        }
    }

    public void Unpause() {
        foreach(TextMeshProUGUI comboText in comboTexts) {
            comboText.enabled = enabledBeforePause;
        }
    }

    public void FailScreen() {
        foreach(TextMeshProUGUI comboText in comboTexts) {
            comboText.enabled = false;
        }
    }

    IEnumerator ComboJuice() {
        yield return null;
    }

}
