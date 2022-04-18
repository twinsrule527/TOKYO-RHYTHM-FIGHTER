using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButton : MonoBehaviour
{

    //a gameobject as clickable button.
    //needs a collider.

    [SerializeField] KeyCode key;

    [SerializeField] UnityEvent functionToCall;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite noHover, hover;
    [SerializeField] Vector3 noHoverScale = new Vector3(1, 1, 1);
    [SerializeField] Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key)) {
            functionToCall.Invoke();
        }
        
        /* else if(Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast()

            if(hit.collider.gameObject 
        }*/
    }

    void OnMouseDown() {
        functionToCall.Invoke();
    }

    void OnMouseEnter() {
        spriteRenderer.sprite = hover;
        //spriteRenderer.gameObject.transform.localScale = hoverScale;
        //transform.localScale = hoverScale;
    }

    void OnMouseExit() {
        spriteRenderer.sprite = noHover;
        //spriteRenderer.gameObject.transform.localScale = noHoverScale;
        //transform.localScale = noHoverScale;
    }
    
}
