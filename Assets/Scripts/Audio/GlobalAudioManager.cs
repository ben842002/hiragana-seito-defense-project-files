using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This class is identical to AudioManager.cs but will contain global sounds such as hovering over a button, turret firing, victory, and defeat.
/// This GameObject will persist through all scenes
/// </summary>
public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager instance;

    [Header("Audio Mixer")]
    [SerializeField] AudioMixerGroup audioMixer;

    [SerializeField] Sound[] globalSounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < globalSounds.Length; i++)
        {
            globalSounds[i].audioSource = gameObject.AddComponent<AudioSource>();
            AudioSource audioS = globalSounds[i].audioSource;

            // copy the settings to the added AudioSource component
            audioS.clip = globalSounds[i].audioClip;
            audioS.volume = globalSounds[i].volume;
            audioS.pitch = globalSounds[i].pitch;
            audioS.outputAudioMixerGroup = audioMixer;
            audioS.loop = globalSounds[i].loop;
        }
    }

    public void Play(string name)
    {
        // Loop through the sounds list to find if parameter name matches with any
        for (int i = 0; i < globalSounds.Length; i++)
        {
            if (globalSounds[i].name == name)
            {
                globalSounds[i].audioSource.Play();
                return;
            }
        }

        // In case sound was not found
        Debug.LogWarning("Sound not found!");
    }

    public void Stop(string name)
    {
        // Loop through the sounds list to find if parameter name matches with any
        for (int i = 0; i < globalSounds.Length; i++)
        {
            if (globalSounds[i].name == name)
            {
                globalSounds[i].audioSource.Stop();
                return;
            }
        }

        // In case sound was not found
        Debug.LogWarning("Sound not found!");
    }
}
