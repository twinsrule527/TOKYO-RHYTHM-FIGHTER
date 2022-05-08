using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class SpeechSequence : MonoBehaviour
{

    //Put this script on the parent of a series of ProgressableBubbles and 
    //give it a text file containing a list of lines.
    //for each line, it grabs the string after the first ': '
    //Each string will be put in each speech bubble, in order. 
    //So, you can have a line like
    //      Sola: She'll be here soon... I should warm up.
    //and it will get 
    //      She'll be here soon... I should warm up.
    //Allows for quick editing of scripts.
    //You still have to make + position the speech bubbles yourself though.
    
    [SerializeField] TextAsset linesFile;

    void Awake()
    {
        FillBubblesFromFile();
    }

    [MenuItem("CUSTOM/Fill speech bubbles from files")]
    static void FillAllBubbles() {
        SpeechSequence [] allSequences = Resources.FindObjectsOfTypeAll<SpeechSequence>();
        foreach(SpeechSequence sequence in allSequences) {
            sequence.FillBubblesFromFile();
        }
    }
    
    public void FillBubblesFromFile() {
        //myText = GetComponent<TextMeshPro>();

        //split the file into lines. 
        string [] linesFromFile = linesFile.text.Split("\n"[0]);

        //grab the children speech bubbles.
        ProgressableBubble [] bubbles = GetComponentsInChildren<ProgressableBubble>(true);

        //for each line, grab the text after the first ':', 
        //and put it in the speech bubbles, in order in scene hierarchy
        int lineIndex = 0;
        for(int i = 0; i < bubbles.Length; i++) {
            
            int colonIndex = linesFromFile[lineIndex].IndexOf(": ");
            if(colonIndex == -1) {
                //Debug.Log("\": \" not found in bubble script file at line " + lineIndex + ", skipping line");
                lineIndex++;
                i--;
                continue;
            } else {
                //all text after ': '
                linesFromFile[lineIndex] = linesFromFile[lineIndex].Substring(colonIndex + 2);
            }
            
            bubbles[i].GetComponentInChildren<TextMeshPro>().text = linesFromFile[lineIndex];
            lineIndex++;
        }
    }
}
