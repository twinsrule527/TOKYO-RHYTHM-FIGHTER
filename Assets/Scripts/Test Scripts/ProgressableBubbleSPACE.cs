using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressableBubbleSPACE : ObjectButton
{

    [SerializeField] GameObject nextObject;
    [SerializeField] bool progressOnSpaceAnywhere = false;
    [SerializeField] bool hideOnSpace = false;

    protected override void Start()
    {
        base.Start();
        if(progressOnSpaceAnywhere || hideOnSpace) {
            keys.Add(KeyCode.Space);
        }
    }

    public void Update()
    {
        

    }

    public override void ButtonPressed()
    {
        if(nextObject != null) {
            nextObject.SetActive(true);
            Debug.Log("Bruh" + nextObject.ToString());
        }

        if(functionToCall != null) {
            base.ButtonPressed();
        }
        
        if(hideOnSpace) {
            this.gameObject.SetActive(false);
        }
    }
}
