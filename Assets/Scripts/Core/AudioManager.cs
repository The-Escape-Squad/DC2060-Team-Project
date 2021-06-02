using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;

    public void PlaySoundOneShot(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }
}
