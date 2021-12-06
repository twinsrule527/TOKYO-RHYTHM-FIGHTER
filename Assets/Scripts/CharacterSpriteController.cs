using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{

    public Sprite spr_Idle;
    public Sprite spr_High;
    public Sprite spr_HighForward;
    public Sprite spr_Low;
    public Sprite spr_LowForward;
    public Sprite spr_MessUp;
    
    
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void idle() {
        spriteRenderer.sprite = spr_Idle;
    }

    public void high() {
        spriteRenderer.sprite = spr_High;
    }

    public void low() {
        spriteRenderer.sprite = spr_Low;
    }

    public void highForward() {
        spriteRenderer.sprite = spr_HighForward;
    }

    public void lowForward() {
        spriteRenderer.sprite = spr_LowForward;
    }

    public void messUp() {
        spriteRenderer.sprite = spr_MessUp;
    }

    public void flashRed() {
        StartCoroutine(flashRedRoutine());
    }

    IEnumerator flashRedRoutine() {
        spriteRenderer.color = Color.red;
        float ttracker = 0;
        while(spriteRenderer.color != Color.white) {
            ttracker += (Time.deltaTime * 1.1f);
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, ttracker);
            yield return null;
        }
        
    }

}
