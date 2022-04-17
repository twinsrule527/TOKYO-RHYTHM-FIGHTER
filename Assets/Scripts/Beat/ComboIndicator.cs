using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboIndicator : MonoBehaviour
{
    
    public TextMeshProUGUI comboText;

    public static int comboCounter = 0;

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
