using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script primarily deals with showing a word on screen in Unity.
public class WordDisplay : MonoBehaviour
{
    // Text Component that is attached to the prefab gameObject
    [HideInInspector] public TMP_Text wordText;

    // This is used in GameMaster.cs. It checks whether word is the active word when an enemy reaches the end.
    [HideInInspector] public Word enemyWord;

    public GameObject enemyParentGameObject;
    [SerializeField] GameObject enemyDeathPrefab;

    /// <summary>
    /// Displays a word on screen by accessing Text component from WordDisplay prefab 
    /// and passing in the parameter hiragana string.
    /// </summary>
    public void SetWord(string hiraganaWord)
    {
        wordText = GetComponent<TMP_Text>();
        wordText.text = hiraganaWord;
    }

    /// <summary>
    /// Removes a certain number of hiragana letters from the text that is shown on screen
    /// </summary>
    public void RemoveHiragana(int numberOfHiragana)
    {   
        wordText.text = wordText.text.Remove(0, numberOfHiragana);
    }

    /// <summary>
    /// Destroys the gameObject that is holding the finished word. Death animation gameObject spawn code is located in WordManager.cs
    /// </summary>
    public void RemoveWord()
    {
        WordManager wm = FindObjectOfType<WordManager>();
        wm.enemyDeathPrefab = enemyDeathPrefab;
        wm.isFlipped = GetComponentInParent<SpriteRenderer>().flipX;

        Destroy(enemyParentGameObject);
    }
}
