using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPicker : MonoBehaviour
{
    TextMeshProUGUI myText;
    [SerializeField] TextAsset textList;

    private string [] linesFromFile;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        linesFromFile = textList.text.Split("\n"[0]);
    }

    void OnEnable() {
        //pick a new string to display every time this obj is enabled 
        int rand = Random.Range(0, linesFromFile.Length);
        myText.text = linesFromFile[rand];
    }
}
