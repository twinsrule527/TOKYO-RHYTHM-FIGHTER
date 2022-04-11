using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPicker : MonoBehaviour
{
    [SerializeField] Text myText;
    [SerializeField] TextAsset textList;

    private string [] linesFromFile;

    void Start()
    {
        linesFromFile = textList.text.Split("\n"[0]);
    }

    void OnEnable() {
        //pick a new string to display every time this obj is enabled 
        int rand = Random.Range(0, linesFromFile.Length);
        myText.text = linesFromFile[rand];
    }
}
