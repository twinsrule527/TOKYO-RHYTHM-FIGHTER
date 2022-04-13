using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Janky Boss Hurt Animation - switches which sprite renderer the boss is using for their hurt animation
public class HurtAnimation : MonoBehaviour
{
    [SerializeField] private float hurtTime;
    [SerializeField] private SpriteRenderer baseRenderer;
    [SerializeField] private SpriteRenderer hurtRenderer;

     SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    public void Hurt() {
        StartCoroutine(Hurts());
    }

    private IEnumerator Hurts() {
        //Switches the spriterenderer being used for the length of the hurt animation

        //TODO: Add ability to have the hurt animation lerp
        hurtRenderer.enabled = true;
        // Change the 'color' property of the 'Sprite Renderer'
        //sprite.color = new Color (255, 1, 1, 1); 

        baseRenderer.enabled = false;
        yield return BeatController.WaitForBeat(BeatController.GetBeat() + hurtTime);
        hurtRenderer.enabled = false;
        baseRenderer.enabled = true;
    }
}
