using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFXController : SFXController
{
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip bossHurtSound;
    public void PlayHurtSound() {
        SfxSync.PlaySoundPerfect(bossHurtSound, bossAudioSource, BeatController.GetBeat());
    }
}
