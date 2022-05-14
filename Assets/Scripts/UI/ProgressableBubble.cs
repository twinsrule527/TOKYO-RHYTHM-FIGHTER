using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressableBubble : ObjectButton
{

    [SerializeField] GameObject nextObject;
    [SerializeField] bool progressOnClickAnywhere = false;
    [SerializeField] bool hideOnClick = false;

    [SerializeField] GameObject hideObjOnAppear;

    protected override void Start()
    {
        base.Start();
        if(progressOnClickAnywhere) {
            keys.Add(KeyCode.Mouse0);
        }
    }

    public override void ButtonPressed()
    {
        if(nextObject != null) {
            nextObject.SetActive(true);
        }

        if(functionToCall != null) {
            base.ButtonPressed();
        }
        
        if(hideOnClick) {
            this.gameObject.SetActive(false);
        }
        if(hideObjOnAppear != null) {
            hideObjOnAppear.SetActive(false);
        }
    }
}
