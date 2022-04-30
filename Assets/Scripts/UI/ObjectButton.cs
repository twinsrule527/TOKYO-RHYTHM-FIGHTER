using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ObjectButton : MonoBehaviour
{

    //a gameobject as clickable button.
    //needs a collider.

    [SerializeField] protected List<KeyCode> keys;

    [SerializeField] protected UnityEvent functionToCall;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite noHover, hover;
    [SerializeField] Transform scaleOnHover;
    [SerializeField] Vector3 noHoverScale = new Vector3(1, 1, 1);
    [SerializeField] Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);

    [SerializeField] int increaseOrderInLayerBy = 0;
    TextMeshPro [] buttonText;
    //public TextMeshPro mainText;

    protected virtual void Start() {
        buttonText = GetComponentsInChildren<TextMeshPro>();
       /* if(buttonText.Length > 0) {
            mainText = buttonText[0];
        }*/
        if(hover == null) {
            hover = spriteRenderer.sprite;
        }
        if(noHover == null) {
            noHover = spriteRenderer.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in keys) {
            if(Input.GetKeyDown(key)) {
                ButtonPressed();
            }
        }
    }

    void OnMouseDown() {
        ButtonPressed();
    }

    protected virtual void ButtonPressed() {
        functionToCall.Invoke();
    }

    void OnMouseEnter() {
        spriteRenderer.sprite = hover;
        scaleOnHover.localScale = hoverScale;
        spriteRenderer.sortingOrder += increaseOrderInLayerBy;
        foreach(TextMeshPro text in buttonText) {
            text.sortingOrder += increaseOrderInLayerBy;
        }
    }

    void OnMouseExit() {
        spriteRenderer.sprite = noHover;
        scaleOnHover.localScale = noHoverScale;
        spriteRenderer.sortingOrder -= increaseOrderInLayerBy;
        foreach(TextMeshPro text in buttonText) {
            text.sortingOrder -= increaseOrderInLayerBy;
        }
    }
    
}
