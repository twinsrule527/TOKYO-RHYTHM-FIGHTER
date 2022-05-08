using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKeySelector : MonoBehaviour
{

    //navigate between buttons selected with keys. 
    //press the right button when the press key is pressed.

    [SerializeField] List<KeyCode> plus, minus;
    [SerializeField] List<KeyCode> pressButton;
    [SerializeField] List<ObjectButton> buttons;
    int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in plus) {
            if(Input.GetKeyDown(key)) {
                int oldIndex = index;
                if(index == 0) {
                    index = buttons.Count - 1;
                } else if(index > 0) {
                    index--;
                } else {
                    index = 0;
                    oldIndex = 0;
                }
                buttons[oldIndex].ButtonDeselected();
                buttons[index].ButtonSelected();
            }
        }
        foreach(KeyCode key in minus) {
            if(Input.GetKeyDown(key)) {
                int oldIndex = index;
                if(index == buttons.Count - 1) {
                    index = 0;
                } else if(index == -1) {
                    index = buttons.Count - 1;
                    oldIndex = buttons.Count - 1;
                } else {
                    index++;
                }
                buttons[oldIndex].ButtonDeselected();
                buttons[index].ButtonSelected();
            }
        }
        foreach(KeyCode key in pressButton) {
            if(Input.GetKeyDown(key)) {
                if(index >= 0) {
                    buttons[index].ButtonPressed();
                }   
            }
        }
    }

    public ObjectButton GetSelectedButton() {
        return buttons[index];
    }
}
