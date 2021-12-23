using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaPronunciation : MonoBehaviour
{   
    public void PlayPronunciation(string name)
    {
        AudioManager.instance.Play(name);
    }
}
