using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Janky Boss Hurt Animation - switches which sprite renderer the boss is using for their hurt animation
public class BossHurtAnimation : MonoBehaviour
{
    [SerializeField] private float hurtTime;
    [SerializeField] private SpriteRenderer bossRenderer;
    [SerializeField] private SpriteRenderer hurtRenderer;
    public void BossHurt() {
        StartCoroutine(BossHurts());
    }

    private IEnumerator BossHurts() {
        //Switches the spriterenderer being used for the length of the hurt animation

        //TODO: Add ability to have the hurt animation lerp
        hurtRenderer.enabled = true;
        bossRenderer.enabled = false;
        yield return BeatController.WaitForBeat(BeatController.GetBeat() + hurtTime);
        hurtRenderer.enabled = false;
        bossRenderer.enabled = true;
    }
}
