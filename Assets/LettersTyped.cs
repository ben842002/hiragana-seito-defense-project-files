using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LettersTyped : MonoBehaviour
{
    public static LettersTyped instance;

    TMP_Text lettersTyped;

    private void Awake()
    {
        instance = this;
        lettersTyped = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        lettersTyped.text = string.Empty;
    }

    /// <summary>
    /// Concatenate correctly typed letters to a string and display it on screen
    /// </summary>
    /// <param name="letter"></param>
    public void DisplayLettersTyped(string romajiTyped)
    {
        romajiTyped = romajiTyped.ToUpper();
        lettersTyped.text = romajiTyped;
    }

    /// <summary>
    /// Resets lettersType text to string.Empty
    /// </summary>
    public void ResetLettersTypedText()
    {
        lettersTyped.text = string.Empty;
    }
}
