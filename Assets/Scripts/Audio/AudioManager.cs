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
            audioS.volume = sounds[i].volume;
            audioS.pitch = sounds[i].pitch;
            audioS.outputAudioMixerGroup = sounds[i].audioMixerGroup;
            audioS.loop = sounds[i].loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
