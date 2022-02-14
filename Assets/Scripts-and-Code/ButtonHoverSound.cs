using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverSound : MonoBehaviour
{
    public void ButtonHover()
    {
        GlobalAudioManager.instance.Play("Button Hover");
    }
}
