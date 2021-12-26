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
    /// </summary>
    public Word(string _romaji, string _hiragana, WordDisplay _display, GameObject wordDisplayParentGameObject)
    {
        romajiWord = _romaji;
        romajiIndex = 0;
        hiraganaLength = _hiragana.Length;
        enemyGameObject = wordDisplayParentGameObject;

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
            // set EnemyDead's isDead boolean to true
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
            // Check if there is even an N and check if its in the NA line or NYA line
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
        // Some hiragana characters translate into more than 1 romaji. TSU for example
        wordDisplay.RemoveHiragana(numberOfHiragana);

        // make the string empty once a particular hiragana is detected (switch cases wont work if you dont)
        romajiTyped = string.Empty;

        Object.FindObjectOfType<WordManager>().TriggerTurret(this);
    }

    // LINK TO HIRAGANA TABLE: https://prnt.sc/1vroxag  
    void HiraganaCheck(string lettersTyped)
    {   
        switch (lettersTyped)
        {
            // HIRAGANA PAIRS
            // kya/gya
            case "kya":
                RemoveHiragana(2);
                break;
            case "kyu":
                RemoveHiragana(2);
                break;
            case "kyo":
                RemoveHiragana(2);
                break;
            case "gya":
                RemoveHiragana(2);
                break;
            case "gyu":
                RemoveHiragana(2);
                break;
            case "gyo":
                RemoveHiragana(2);
                break;

            // nya/hya
            case "nya":
                RemoveHiragana(2);
                break;
            case "nyu":
                RemoveHiragana(2);
                break;
            case "nyo":
                RemoveHiragana(2);
                break;
            case "hya":
                RemoveHiragana(2);
                break;
            case "hyu":
                RemoveHiragana(2);
                break;
            case "hyo":
                RemoveHiragana(2);
                break;

            // bya/pya
            case "bya":
                RemoveHiragana(2);
                break;
            case "byu":
                RemoveHiragana(2);
                break;
            case "byo":
                RemoveHiragana(2);
                break;
            case "pya":
                RemoveHiragana(2);
                break;
            case "pyu":
                RemoveHiragana(2);
                break;
            case "pyo":
                RemoveHiragana(2);
                break;

            // mya/rya
            case "mya":
                RemoveHiragana(2);
                break;
            case "myu":
                RemoveHiragana(2);
                break;
            case "myo":
                RemoveHiragana(2);
                break;
            case "rya":
                RemoveHiragana(2);
                break;
            case "ryu":
                RemoveHiragana(2);
                break;
            case "ryo":
                RemoveHiragana(2);
                break;

            // ja/cha
            case "ja":
                RemoveHiragana(2);
                break;
            case "ju":
                RemoveHiragana(2);
                break;
            case "je":
                RemoveHiragana(2);
                break;
            case "jo":
                RemoveHiragana(2);
                break;
            case "cha":
                RemoveHiragana(2);
                break;
            case "chu":
                RemoveHiragana(2);
                break;
            case "che":
                RemoveHiragana(2);
                break;
            case "cho":
                RemoveHiragana(2);
                break;

            // SHA
            case "sha":
                RemoveHiragana(2);
                break;
            case "shu":
                RemoveHiragana(2);
                break;
            case "she":
                RemoveHiragana(2);
                break;
            case "sho":
                RemoveHiragana(2);
                break;

            // -------------------
            // SPECIAL CASES
            case "wa":
                RemoveHiragana(1);
                break;
            case "wo":
                RemoveHiragana(1);
                break;
            case "n":
                RemoveHiragana(1);
                break;

            // --------------------
            // A LINE
            case "a":
                RemoveHiragana(1);
                break;
            case "i":
                RemoveHiragana(1);
                break;
            case "u":
                RemoveHiragana(1);
                break;
            case "e":
                RemoveHiragana(1);
                break;
            case "o":
                RemoveHiragana(1);
                break;

            // KA/GA LINE
            case "ka":
                RemoveHiragana(1);
                break;
            case "ki":
                RemoveHiragana(1);
                break;
            case "ku":
                RemoveHiragana(1);
                break;
            case "ke":
                RemoveHiragana(1);
                break;
            case "ko":
                RemoveHiragana(1);
                break;
            case "ga":
                RemoveHiragana(1);
                break;
            case "gi":
                RemoveHiragana(1);
                break;
            case "gu":
                RemoveHiragana(1);
                break;
            case "ge":
                RemoveHiragana(1);
                break;
            case "go":
                RemoveHiragana(1);
                break;

            // SA/ZA LINE
            case "sa":
                RemoveHiragana(1);
                break;
            case "shi":
                RemoveHiragana(1);
                break;
            case "su":
                RemoveHiragana(1);
                break;
            case "se":
                RemoveHiragana(1);
                break;
            case "so":
                RemoveHiragana(1);
                break;
            case "za":
                RemoveHiragana(1);
                break;
            case "ji":
                RemoveHiragana(1);
                break;
            case "zu":
                RemoveHiragana(1);
                break;
            case "ze":
                RemoveHiragana(1);
                break;
            case "zo":
                RemoveHiragana(1);
                break;

            // TA/DA LINE
            case "ta":
                RemoveHiragana(1);
                break;
            case "chi":
                RemoveHiragana(1);
                break;
            case "tsu":
                RemoveHiragana(1);
                break;
            case "te":
                RemoveHiragana(1);
                break;
            case "to":
                RemoveHiragana(1);
                break;
            case "da":
                RemoveHiragana(1);
                break;
            case "de":
                RemoveHiragana(1);
                break;
            case "do":
                RemoveHiragana(1);
                break;

            // NA LINE
            case "na":
                RemoveHiragana(1);
                break;
            case "ni":
                RemoveHiragana(1);
                break;
            case "nu":
                RemoveHiragana(1);
                break;
            case "ne":
                RemoveHiragana(1);
                break;
            case "no":
                RemoveHiragana(1);
                break;

            // HA LINE
            case "ha":
                RemoveHiragana(1);
                break;
            case "hi":
                RemoveHiragana(1);
                break;
            case "fu":
                RemoveHiragana(1);
                break;
            case "hu":
                RemoveHiragana(1);
                break;
            case "he":
                RemoveHiragana(1);
                break;
            case "ho":
                RemoveHiragana(1);
                break;

            // BA LINE
            case "ba":
                RemoveHiragana(1);
                break;
            case "bi":
                RemoveHiragana(1);
                break;
            case "bu":
                RemoveHiragana(1);
                break;
            case "be":
                RemoveHiragana(1);
                break;
            case "bo":
                RemoveHiragana(1);
                break;

            // PA LINE
            case "pa":
                RemoveHiragana(1);
                break;
            case "pi":
                RemoveHiragana(1);
                break;
            case "pu":
                RemoveHiragana(1);
                break;
            case "pe":
                RemoveHiragana(1);
                break;
            case "po":
                RemoveHiragana(1);
                break;

            // MA LINE
            case "ma":
                RemoveHiragana(1);
                break;
            case "mi":
                RemoveHiragana(1);
                break;
            case "mu":
                RemoveHiragana(1);
                break;
            case "me":
                RemoveHiragana(1);
                break;
            case "mo":
                RemoveHiragana(1);
                break;

            // YA LINE
            case "ya":
                RemoveHiragana(1);
                break;
            case "yu":
                RemoveHiragana(1);
                break;
            case "yo":
                RemoveHiragana(1);
                break;

            // RA LINE
            case "ra":
                RemoveHiragana(1);
                break;
            case "ri":
                RemoveHiragana(1);
                break;
            case "ru":
                RemoveHiragana(1);
                break;
            case "re":
                RemoveHiragana(1);
                break;
            case "ro":
                RemoveHiragana(1);
                break;
        }
    }
}
