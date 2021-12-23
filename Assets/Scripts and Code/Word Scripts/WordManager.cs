using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    WordSpawner wordSpawner;
    WordGenerator wordGenerator;
    TokensText tokensText;

    [SerializeField] Animator[] turrets;

    [Header("Main List to store Words")]
    public List<Word> words;

    [Header("Active Word")]
    public bool hasActiveWord;
    public Word activeWord;

    private void Awake()
    {
        wordSpawner = GetComponent<WordSpawner>();
        wordGenerator = GetComponent<WordGenerator>();
        tokensText = FindObjectOfType<TokensText>();
    }

    public void SpawnEnemyWord(GameObject enemyPrefab, Vector2 spawnPos, string romaji, string hiragana)
    {
        // choose a random index and spawn a wordDisplay. We will pass these into the Word Constructor
        WordDisplay wordDisplay = wordSpawner.SpawnWordDisplay(enemyPrefab, spawnPos);
        GameObject enemyGameObject = wordDisplay.transform.parent.parent.gameObject;

        // ------------------------------------------------------------
        // The player will type english letters (romaji) but hiragana will show on screen by passing it in the wordDisplay
        Word word = new Word(romaji, hiragana, wordDisplay, enemyGameObject);

        // assign the word to its wordDisplay's enemyWord variable. enemyWord is used when the enemy reaches the end. We want to remove the word from the list
        word.wordDisplay.enemyWord = word;
        // ------------------------------------------------------------

        // add word to main words list
        words.Add(word);
    }

    /// <summary>
    /// Initial function made that reads words from a file and picks a randomly selected one
    /// </summary>
    public void SpawnRandomEnemyWord(GameObject enemyPrefab, Vector2 spawnPos)
    {
        // choose a random index and spawn a wordDisplay. We will pass these into the Word Constructor
        int randomIndex = wordGenerator.GetRandomWordIndex();
        WordDisplay wordDisplay = wordSpawner.SpawnWordDisplay(enemyPrefab, spawnPos);
        GameObject enemyGameObject = wordDisplay.transform.parent.parent.gameObject;

        // ------------------------------------------------------------
        // The player will type english letters (romaji) but hiragana will show on screen by passing it in the wordDisplay
        string romaji = wordGenerator.romajiList[randomIndex];
        string hiragana = wordGenerator.hiraganaList[randomIndex];
        Word word = new Word(romaji, hiragana, wordDisplay, enemyGameObject);

        // assign the word to its wordDisplay's enemyWord variable. enemyWord is used when the enemy reaches the end. We want to remove the word from the list
        word.wordDisplay.enemyWord = word;
        // ------------------------------------------------------------

        // add word to main words list
        words.Add(word);
    }

    /// <summary>
    /// Detects if player is typing a new word or an existing word.
    /// </summary>
    public void TypeLetter(char letter)
    {   
        // First, check if the letter typed is an existing word OR a new word
        if (hasActiveWord == true)
        {
            // check if the input is the next letter. If so, it is CORRECT and change color to green. Remove letter from word
            if (activeWord.GetNextLetter() == letter)
            {
                TriggerTurret(activeWord);
                activeWord.wordDisplay.wordText.color = Color.green;
                activeWord.TypeLetter();
            }
            else
            {
                // change color to red to indicate that its WRONG
                activeWord.wordDisplay.wordText.color = Color.red;
            }
        }
        else
        {   
            // loop through words array and find if a word's starting letter matches the letter the player typed
            for (int i = 0; i < words.Count; i++)
            {   
                if (words[i].GetNextLetter() == letter)
                {   
                    // set the matching word as the new active word;
                    activeWord = words[i];
                    hasActiveWord = true;
                    TriggerTurret(activeWord);

                    // set word's canvas object sorting layer higher as its the important word
                    activeWord.wordDisplay.GetComponentInParent<Canvas>().sortingOrder = 1;
                    
                    // change active word's color and register first letter input
                    words[i].wordDisplay.wordText.color = Color.green;
                    words[i].TypeLetter();

                    break;
                }
            }
        }

        // check if player has completed in typing the active word
        if (hasActiveWord == true && activeWord.WordTyped() == true)
        {   
            tokensText.AddTokens(activeWord.word.Length);

            // set active word boolean to false as the word has been completed and delete the active word from words list
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

    void TriggerTurret(Word word)
    {   
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].SetTrigger("Shoot");

            // reference the active word's gameObject
            Transform enemyTarget = word.enemyGameObject.transform;
            turrets[i].GetComponentInParent<Turret>().RotateTurret(enemyTarget);

            // spawn bullet instance and give target gameObject
            GameObject bullet = Instantiate(turrets[i].GetComponentInParent<Turret>().bulletPrefab, turrets[i].transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().enemy = enemyTarget;
        }
    }
}
