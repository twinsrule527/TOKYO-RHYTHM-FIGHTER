using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFXController : SFXController
{
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private List<AudioClip> bossHurtSounds;
    [SerializeField] private List<AudioClip> bossAttackSounds;

    void Start() {
    }
    public void PlayHurtSound() {
        int rnd = Random.Range(0, bossHurtSounds.Count);
        SfxSync.PlaySoundPerfect(bossHurtSounds[rnd], bossAudioSource, BeatController.GetBeat());
    }

    //Playing an attack sound, you can either have the sound be random, or choose a sound to play
    public void PlayAttackSound(int atkNum = -1) {
        if(atkNum == -1 || atkNum >= bossAttackSounds.Count) {
            atkNum = Random.Range(0, bossAttackSounds.Count);
        }
        SfxSync.PlaySoundPerfect(bossAttackSounds[atkNum], bossAudioSource, BeatController.GetBeat());
    }
}
