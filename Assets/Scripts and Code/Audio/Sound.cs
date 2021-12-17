using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    [Range(0, 1)]
    public float volume;

    [Range(-3, 3)]
    public float pitch;

    public AudioMixerGroup audioMixerGroup;
    public bool loop;

    [HideInInspector] public AudioSource audioSource;
}
