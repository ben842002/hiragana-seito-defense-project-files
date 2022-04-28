using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    WordManager wordManager;

    void Awake()
    {
        wordManager = GetComponent<WordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // get keyboard input that was entered during a frame
        string frameInput = Input.inputString;
        for (int i = 0; i < frameInput.Length; i++)
        {
            // check if input matches with a letter on the active word. If there is no active word, check if any word in the list
            // starts with the letter that was typed
            char letter = frameInput[i];
            wordManager.TypeLetter(letter);
        }
    }
}
