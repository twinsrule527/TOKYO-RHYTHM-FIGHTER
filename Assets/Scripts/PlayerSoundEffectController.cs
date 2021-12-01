using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectController : MonoBehaviour
{

    public AudioSource src_step;
    public AudioClip sfx_step;

    public AudioSource src_blockHigh;
    public AudioClip sfx_blockHigh;

    public AudioSource src_blockLow;
    public AudioClip sfx_blockLow;

    public AudioSource src_combo;
    public AudioClip[] sfx_combos;

    public AudioSource src_switchHigh;
    public AudioClip sfx_switchHigh;

    public AudioSource src_switchLow;
    public AudioClip sfx_switchLow;

    public AudioSource src_attackHigh;
    public AudioClip sfx_attackHigh;

    public AudioSource src_attackLow;
    public AudioClip sfx_attackLow;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sfx_Step() {
        src_step.Play();
    }

    //aka clash
    public void Sfx_BlockHigh() {
        src_blockHigh.Play();
    }

    public void Sfx_BlockLow()
    {
        src_blockLow.Play();
    }

    public void Sfx_Combo(int num) {

    }

    public void Sfx_High() {

    }

    public void Sfx_Low() {

    }

    public void Sfx_HighForward() {

    }

    public void Sfx_LowForward() {

    }


}
