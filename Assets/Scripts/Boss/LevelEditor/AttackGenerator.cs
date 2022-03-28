using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{

    //dictionary pairing BossAttack files to characters

    //TODO: HOW TO PUT SCRIPTS IN THIS DICTIONARY IN INSPECTOR. 
    //ACTUALLY, PREFABS ARE IN THE BOSS PREFAB. 
    

    [SerializeField] char [] symbols;
    [SerializeField] BossAttack [] attacks;


    // Start is called before the first frame update
    void Start()
    {
        if(symbols.Length != attacks.Length) {
            Debug.Log("ERROR!!! list of symbols is of different length than list of attacks in level generator dictionary");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
