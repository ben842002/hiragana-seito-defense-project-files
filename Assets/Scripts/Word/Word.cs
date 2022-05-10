using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Word
{
    // contains the actual word in ROMAJI
    public string romajiWord;

    // int index to keep track of typing (basically what position we are at in a word)
    private int romajiIndex;

    // keep track of correct romaji. This will be used to check if it matches any hiragana letters
    private string romajiTyped;

    // this array is used in grammar special case 1
    readonly char[] letters = { 'a', 'i', 'u', 'e', 'o', 'y' };

    // -----------------------------------------------------------------------------

    // store gameObject so turrets correctly shoot at target when typing
    public GameObject enemyGameObject;

    // store the size of the hiragana string. This value is used when adding tokens after an enemy has been killed
    [HideInInspector] public int hiraganaLength;

    // shows the word on screen (reason why its public is that we access this in TypeLetter() in WordManager
    [HideInInspector] public WordDisplay wordDisplay;

    /// <summary>
    /// Constructor (initializer) 
    /// 1.) Sets word variable to romaji (1st parameter) and will be the word that the player actually types. 
    /// 2.) Sets wordDisplay variable to _display (3rd parameter)
    /// 3.) Passes in hiragana (2nd parameter) into wordDisplay to display the hiragana word on screen
    /// 4.) Stores wordDisplay's parent gameObject into a variable called enemyGameObject. This will be used when a bullet spawns so that it can travel to the right target.
    /// 5.) Sets the waypoint path the enemy will take. This is included here to account for when enemies in the scenes have different paths from one another
    /// </summary>
    public Word(string _romaji, string _hiragana, WordDisplay _display, GameObject wordDisplayParentGameObject, Waypoints _waypoints)
    {
        romajiWord = _romaji;
        romajiIndex = 0;
        hiraganaLength = _hiragana.Length;
        enemyGameObject = wordDisplayParentGameObject;

        // assign this enemy a particular waypoint path
        enemyGameObject.GetComponent<EnemyMovement>().wayP = _waypoints;

        // assign wordDisplay and then use wordDisplay to display hiragana on screen 
        wordDisplay = _display;
        wordDisplay.SetWord(_hiragana);
    }

    /// <summary>
    /// Returns next letter (char) in a word.
    /// </summary>
    public char GetNextLetter()
    {
        return romajiWord[romajiIndex];
    }

    /// <summary>
    /// Checks if word has been fully typed.
    /// </summary>
    public bool WordTyped()
    {
        bool wordTyped = romajiIndex >= romajiWord.Length;
        if (wordTyped == true)
        {
            wordDisplay.RemoveWord();
        }

        return wordTyped;
    }

    /// <summary>
    /// This function will let system know that a letter has been typed (increases romajiIndex)
    /// </summary>
    public void TypeLetter()
    {   
        // store romaji that was typed into a temporary string list
        romajiTyped += romajiWord[romajiIndex];

        // move to the next character in the romaji
        romajiIndex++;

        // Special Cases
        if (romajiIndex < romajiWord.Length)
        {
            // GRAMMAR SPECIAL CASE 1: Differentiate NA line and N. 
            // Check if there is an N and check if its in the NA line or NYA line
            if (romajiTyped == "n" && letters.Contains(romajiWord[romajiIndex]))
                return;

            // GRAMMAR SPECIAL CASE 2: Double Consonant (Example: TEKKEN)
            // Check if the next letter is the same as the current letter that was just typed
            if (romajiWord[romajiIndex] == romajiWord[romajiIndex - 1])
                RemoveHiragana(1);
        }

        // check if lettersTyped is equivalent to any hiragana. If so, remove the hiragana
        HiraganaCheck(romajiTyped);
    }

    // Function is called in HiraganaCheck
    void RemoveHiragana(int numberOfHiragana)
    {   
        // only shoot when we haven't completed a word (there is a bug where a bullet spawns and goes to the wrong target)
        if (WordTyped() == false)
        {   
            // find WordManager script and trigger turret
            WordManager wm = Object.FindObjectOfType<WordManager>();
            wm.TriggerTurret(enemyGameObject);
        }

        // Some hiragana characters translate into more than 1 romaji. TSU for example
        wordDisplay.RemoveHiragana(numberOfHiragana);

        // make the string empty once a particular hiragana is detected (switch cases wont work if you dont)
        romajiTyped = string.Empty;
    }

    /// <summary>
    /// Checks if the letters typed by the user matches any romaji in the hiragana table
    /// LINK TO HIRAGANA TABLE: https://prnt.sc/1vroxag  
    /// </summary>
    /// <param name="lettersTyped"></param>
    void HiraganaCheck(string lettersTyped)
    {   
        Dictionary<string, int> hiraganaTable = HiraganaTable.hiraganaTable;
        if (hiraganaTable.ContainsKey(lettersTyped))
            RemoveHiragana(hiraganaTable[lettersTyped]);            
    }
}
