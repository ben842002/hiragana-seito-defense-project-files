using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text hiraganaLineText;
    [SerializeField] TMP_Text dialogueText;

    // First In, First Out
    private Queue<string> sentences = new Queue<string>();

    public void StartDialogue(Dialogue dialogue)
    {
        // change default text into given text
        titleText.text = dialogue.name;
        hiraganaLineText.text = dialogue.hiraganaLineText;

        // clears dialogue box if there was any previous sentences
        sentences.Clear();

        foreach (string sentence in dialogue.dialogueSentences)
        {   
            // queue up the sentences
            sentences.Enqueue(sentence);
        }

        // display first sentence
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // if dialogue finishes
        if (sentences.Count == 0)
        {
            return;
        }

        // get the next sentence
        string sentence = sentences.Dequeue();

        // StopAllCoroutines() stops TypeSentence in the case that the player goes to the next sentence while the sentence is still typing
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        // optional code to display text automatically
        //dialogueText.text = sentence;
    }

    // this ienumator is for the text animation of a character one by one
    IEnumerator TypeSentence(string sentence)
    {
        // make dialogueText text empty
        dialogueText.text = "";

        // turn sentence into an array (characters make up one element each) by using .ToCharArray()
        foreach (char letter in sentence.ToCharArray())
        {
            // add each letter(element) into dialogueText string one by one
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
