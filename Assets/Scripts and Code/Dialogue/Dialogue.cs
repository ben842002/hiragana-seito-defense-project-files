using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public string hiraganaLineText;

    [TextArea(3, 9)]
    public string[] dialogueSentences;
}
