using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaFadeAnimation : MonoBehaviour
{
    [HideInInspector] public GameObject otherHiraganaObject;

    /// <summary>
    /// This function is called through an OnClick event for a button. The parameter _otherHiraganaObject is used to determine
    /// which object should be enabled in the scene after this object's fade out animation has finished playing.
    /// </summary>
    /// <param name="_otherHiraganaObject"></param>
    public void TriggerAnimation(GameObject _otherHiraganaObject)
    {
        otherHiraganaObject = _otherHiraganaObject;
        GetComponent<Animator>().SetTrigger("Fade");
    }
}
