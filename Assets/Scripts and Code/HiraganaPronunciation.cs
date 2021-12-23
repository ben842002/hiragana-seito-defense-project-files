using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaPronunciation : MonoBehaviour
{   
    // TRY TO OPTIMIZE THIS SCRIPT
    [SerializeField] string[] audioNames;

    public void Play1st()
    {
        AudioManager.instance.Play(audioNames[0]);
    }

    public void Play2nd()
    {
        AudioManager.instance.Play(audioNames[1]);
    }

    public void Play3rd()
    {
        AudioManager.instance.Play(audioNames[2]);
    }

    public void Play4th()
    {
        AudioManager.instance.Play(audioNames[3]);
    }

    public void Play5th()
    {
        AudioManager.instance.Play(audioNames[4]);
    }
}
