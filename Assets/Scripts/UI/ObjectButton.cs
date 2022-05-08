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

    protected virtual void Start() {
        buttonText = GetComponentsInChildren<TextMeshPro>();
        if(hover == null) {
            hover = spriteRenderer.sprite;
        }
        if(noHover == null) {
            noHover = spriteRenderer.sprite;
        }
        if(noHoverScale.Equals(hoverScale)) {
            noHoverScale = scaleOnHover.localScale;
            hoverScale = scaleOnHover.localScale;
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

    public virtual void ButtonPressed() {
        functionToCall.Invoke();
    }

    public virtual void ButtonSelected() {
        spriteRenderer.sprite = hover;
        scaleOnHover.localScale = hoverScale;
        spriteRenderer.sortingOrder += increaseOrderInLayerBy;
        foreach(TextMeshPro text in buttonText) {
            text.sortingOrder += increaseOrderInLayerBy;
        }
    }

    public virtual void ButtonDeselected() {
        spriteRenderer.sprite = noHover;
        scaleOnHover.localScale = noHoverScale;
        spriteRenderer.sortingOrder -= increaseOrderInLayerBy;
        foreach(TextMeshPro text in buttonText) {
            text.sortingOrder -= increaseOrderInLayerBy;
        }
    }

    void OnMouseEnter() {
        ButtonSelected();
    }

    void OnMouseExit() {
        ButtonDeselected();
    }
    
}
