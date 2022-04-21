using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : SFXController
{
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private List<AudioClip> playerHurtSounds;
    [SerializeField] private List<AudioClip> playerOnBeatSounds;
    public void PlayHurtSound() {
        int rnd = Random.Range(0, playerHurtSounds.Count);
        SfxSync.PlaySoundPerfect(playerHurtSounds[rnd], playerAudioSource, BeatController.GetBeat());
    
    }

    public void PlayOnBeatSound() {
        Debug.Log("PLAYSOUND");
        int rnd = Random.Range(0, playerOnBeatSounds.Count);
        SfxSync.PlaySoundPerfect(playerOnBeatSounds[rnd], playerAudioSource, BeatController.GetBeat());
    
    }
}
