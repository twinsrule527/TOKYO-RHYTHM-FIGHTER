using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButton : MonoBehaviour
{

    //a gameobject as clickable button.
    //needs a collider.

    [SerializeField] KeyCode [] keys;

    [SerializeField] UnityEvent functionToCall;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite noHover, hover;
    [SerializeField] Transform scaleOnHover;
    [SerializeField] Vector3 noHoverScale = new Vector3(1, 1, 1);
    [SerializeField] Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in keys) {
            if(Input.GetKeyDown(key)) {
                functionToCall.Invoke();
            }
        }
        
    }

    void OnMouseDown() {
        functionToCall.Invoke();
    }

    void OnMouseEnter() {
        spriteRenderer.sprite = hover;
        scaleOnHover.localScale = hoverScale;
    }

    void OnMouseExit() {
        spriteRenderer.sprite = noHover;
        scaleOnHover.localScale = noHoverScale;
    }
    
}
