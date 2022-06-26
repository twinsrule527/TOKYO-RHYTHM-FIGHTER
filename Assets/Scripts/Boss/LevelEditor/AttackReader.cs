using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackReader : MonoBehaviour
{
    //this is attached to a boss AI prefab.

    //RULES FOR TEXT:
    //symbols must be made up of the same char. AAA is valid but ABB is not.
    //don't use spaces

    //the attack patterns for this boss.
    //[x][y] is the yth file corresponding to the xth boss stage.
    [SerializeField] List<TextAsset> stage0Tutorial0, stage1Tutorial1, stage2Tutorial2, stage3Tutorial3, stage4EndTutorial, stage5StartGameplay, stage6Battle1; 
    //add more stages as needed 
    List<List<TextAsset>> patternsText = new List<List<TextAsset>>();
    public List<List<TextAsset>> PatternsText {
        get {
            return patternsText;
        }
    }
    void Awake() {
        patternsText.Add(stage0Tutorial0); //dont forget to add stages here 
        patternsText.Add(stage1Tutorial1);
        patternsText.Add(stage2Tutorial2);
        patternsText.Add(stage3Tutorial3);
        patternsText.Add(stage4EndTutorial);
        patternsText.Add(stage5StartGameplay);
        patternsText.Add(stage6Battle1);
    }

    //pass in the stage number (starting from 0) of attacks to get.
    public List<List<char>> GetPatterns(int stage)
    {
        //read the object names of all the attacks children of the object.

        BossAttack[] attackObjs = GetComponentsInChildren<BossAttack>();
        char[] attackChars = new char[attackObjs.Length];
        int[] charRepeatNum = new int[attackObjs.Length];

        //the first piece of the name before the space will be what this attack is in the text files.
        //reading at runtime so these can be changed on the fly.
        for(int i = 0; i < attackObjs.Length; i++) {
            //save the first character, and how many times it repeats 
            attackChars[i] = attackObjs[i].name[0];
            for(int j = 0; attackObjs[i].name[j] != ' '; j++) {
                charRepeatNum[i] = j + 1;
            }
        }
        
        //generate char lists for each attack pattern from a file.
        List<List<char>> patterns = new List<List<char>>();

        //collect all lines from all files in one list 
        List<string> linesFromFile = new List<string>();
        foreach(TextAsset txt in patternsText[stage]) {
            string[] linesFromFileArr = txt.text.Split("\n"[0]);
            foreach(string s in linesFromFileArr) {
                linesFromFile.Add(s);
            }
        }
        
        //for each line
        foreach (string line in linesFromFile) {

            List<char> currentPattern = new List<char>();

            //look for patterns
            for(int i = 0; i < line.Length - 1; i++) { //-1 cause dont hit ending char

                int symbolIndex = Array.IndexOf(attackChars, line[i]);
                if(symbolIndex >= 0) { //if symbol exists 

                    //make sure the length is correct, and move to the end of the long symbol
                    for(int j = 1; j < charRepeatNum[symbolIndex]; j++) {
                        i++; //we're moving forward through the string
                        if(line[i] != attackChars[symbolIndex]) {
                            //if find a broken pattern, throw an error and burn the whole place down 
                            Debug.Log("ERROR!!!!! a symbol in the boss attack text file at index " + i + " didn't have the correct length. now the attack will be messed up!!!");
                        }
                    }
                    //when find a pattern, add the matching char
                    currentPattern.Add(line[i]);

                } else {
                    Debug.Log("ERROR!!!!!! symbol in boss attack text file at index " + i + " is not one of the symbols at the start of the names of the BossAttack objects that are children in the BossAI prefab!!!");
                }
            }

            //attack pattern finished. add to list of patterns, move on to next line 
            patterns.Add(currentPattern);
        }

        return patterns;
    }

    //TODO- should this object also disable or destroy itself when done, for efficiency?
    
    //temporary for debugging
    //TODO delete later
    /*
    void Start() {
        List<List<char>> chars = GetPatterns();
        foreach(List<char> cl in chars) {
            string line = "";
            foreach(char c in cl) {
                line = line + c;
            }
            Debug.Log(line);
        }
    } */
}
