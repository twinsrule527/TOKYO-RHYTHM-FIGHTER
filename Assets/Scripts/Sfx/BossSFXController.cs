using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFXController : SFXController
{
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private List<AudioClip> bossHurtSounds;
    [SerializeField] private List<AudioClip> bossAttackSounds;

    int dontPlayOnThisBeat = -1; //this is hardcoded to only work on on-beat attacks- would have to change if adding off beat attacks (!!!)

    float volumeNormal = 1;
    float volumeLow = 0.5f;

    void Start() {
    }
    public void PlayHurtSound() {

        if((int)(BeatController.GetNearestBeat()) == dontPlayOnThisBeat) {
            //return;
        } else {
            bossAudioSource.volume = volumeNormal;
        }

        int rnd = Random.Range(0, bossHurtSounds.Count);
        SfxSync.PlaySoundPerfect(bossHurtSounds[rnd], bossAudioSource, BeatController.GetBeat());
    }

    //Playing an attack sound, you can either have the sound be random, or choose a sound to play
    public void PlayAttackSound(int atkNum = -1) {

        if((int)(BeatController.GetNearestBeat()) == dontPlayOnThisBeat) {
            //return;
        } else {
            bossAudioSource.volume = volumeNormal;
        }

        if(atkNum == -1 || atkNum >= bossAttackSounds.Count) {
            atkNum = Random.Range(0, bossAttackSounds.Count);
        }
        SfxSync.PlaySoundPerfect(bossAttackSounds[atkNum], bossAudioSource, BeatController.GetBeat());
    }


    //if the player successfully blocks an attack
    //ACTUALLY, instead of cancelling, just lowers the volume. 
    //(could also try changing the "priority"?)
    public void CancelSound() {

        bossAudioSource.volume = volumeLow;

        //if already playing, stop
        if(bossAudioSource.isPlaying) {
            //bossAudioSource.Stop();

        } else {
            //if not playing, mark this beat as not to play a sound on 
            dontPlayOnThisBeat = (int)(BeatController.GetNearestBeat());
        }
    }
}
