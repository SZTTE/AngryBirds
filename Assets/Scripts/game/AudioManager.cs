using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource _audioSource;
    AudioManager()
    {
        Instance = this;
    }
    public void Play(AudioClip audioClip, float volume)
    {
        _audioSource.PlayOneShot(audioClip,volume);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
