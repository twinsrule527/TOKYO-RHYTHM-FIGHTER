using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectController : MonoBehaviour
{

    public AudioSource src_stepForward;
    public AudioClip sfx_stepForward;

    public AudioSource src_stepBack;
    public AudioClip sfx_stepBack;

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

    public AudioSource src_hit;
    public AudioClip sfx_hit;

    public AudioSource src_missBeat;
    public AudioClip sfx_missBeat;

    public AudioSource src_cantForward;
    public AudioClip sfx_cantForward;

    public AudioSource src_cantAttack;
    public AudioClip sfx_cantAttack;

    public float vol = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sfx_StepForward() {
        src_stepForward.PlayOneShot(sfx_stepForward, vol);
    }

    public void Sfx_StepBack()
    {
        src_stepBack.PlayOneShot(sfx_stepBack, vol);
    }

    //aka clash
    public void Sfx_BlockHigh() {
        src_blockHigh.PlayOneShot(sfx_blockHigh, vol);
    }

    public void Sfx_BlockLow()
    {
        src_blockLow.PlayOneShot(sfx_blockLow, vol);
    }

    //num refers to the combo size, maxing out at 8
    public void Sfx_Combo(int num) {
        src_combo.PlayOneShot(sfx_combos[num], vol);
    }

    //aka swipe, used when switching between high and low stances
    public void Sfx_High() {
        src_switchHigh.PlayOneShot(sfx_switchHigh, vol);
    }

    public void Sfx_Low() {
        src_switchLow.PlayOneShot(sfx_switchLow, vol);
    }

    //high and low attack
    public void Sfx_HighForward() {
        src_attackHigh.PlayOneShot(sfx_attackHigh, vol);
    }

    public void Sfx_LowForward() {
        src_attackLow.PlayOneShot(sfx_attackLow, vol);
    }

    //successful hit, deals damage
    public void Sfx_Hit()
    {
        src_hit.PlayOneShot(sfx_hit, vol);
    }

    //missed beat, not necessarily missed attack
    public void Sfx_MissBeat()
    {
        src_missBeat.PlayOneShot(sfx_missBeat, vol);
    }

    public void Sfx_CantForward() {
        src_cantForward.PlayOneShot(sfx_cantForward, vol);
    }

    public void Sfx_CantAttack() {
        src_cantAttack.PlayOneShot(sfx_cantAttack, vol); 
    }

}
