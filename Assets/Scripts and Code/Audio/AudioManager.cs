using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sounds Array")]
    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // initialize sounds
        for (int i = 0; i < sounds.Length; i++)
        {   
            // add AudioSource component for each element in sounds array
            sounds[i].audioSource = gameObject.AddComponent<AudioSource>();

            // copy the settings to the added AudioSource component
            AudioSource audioS = sounds[i].audioSource;
            audioS.clip = sounds[i].audioClip;
            audioS.volume = sounds[i].volume;
            audioS.pitch = sounds[i].pitch;
            audioS.outputAudioMixerGroup = sounds[i].audioMixerGroup;
            audioS.loop = sounds[i].loop;
        }
    }

    public void Play(string name)
    {   
        // Loop through the sounds list to find if parameter name matches with any
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].audioSource.Play();
                return;
            }        
        }

        // In case sound was not found
        Debug.LogWarning("Sound not found!");
    }
}
