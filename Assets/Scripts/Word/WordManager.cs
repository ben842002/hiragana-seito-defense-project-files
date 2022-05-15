using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    WordSpawner wordSpawner;

    // this is also referenced in InventoryManager.cs for EnemyDestroyer
    [HideInInspector] public TokensText tokensText;

    // these are constantly constantly updated every time an enemy dies. The enemy passes it's enemyDeathPrefab and boolean value to these variable
    // See WordDisplay.cs and RemoveWord() for specifics
    [HideInInspector] public GameObject enemyDeathPrefab;
    [HideInInspector] public bool isFlipped;

    [Header("Turret Animators")]
    [SerializeField] Animator[] turrets;
    int turretIndex = 0;    // determines what turret in the scene will shoot

    [Header("Main List to store Words")]
    public List<Word> words;

    [Header("Active Word")]
    public bool hasActiveWord;
    [HideInInspector] public Word activeWord;

    private void Awake()
    {
        wordSpawner = GetComponent<WordSpawner>();
        tokensText = FindObjectOfType<TokensText>();
    }

    public void SpawnEnemyWord(GameObject enemyPrefab, Vector2 spawnPos, string romaji, string hiragana, Waypoints waypoints)
    {
        // choose a random index and spawn a wordDisplay. We will pass these into the Word Constructor
        WordDisplay wordDisplay = wordSpawner.SpawnWordDisplay(enemyPrefab, spawnPos);
        GameObject enemyGameObject = wordDisplay.transform.parent.parent.gameObject;

        // ------------------------------------------------------------
        // The player will type english letters (romaji) but hiragana will show on screen by passing it in the wordDisplay
        Word word = new Word(romaji, hiragana, wordDisplay, enemyGameObject, waypoints);

        // assign the word to its wordDisplay's enemyWord variable. enemyWord is used when the enemy reaches the end. We want to remove the word from the words list
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
            // spawn enemy death animation
            GameObject enemyDeath = Instantiate(enemyDeathPrefab, activeWord.enemyGameObject.transform.position, Quaternion.identity);
            if (isFlipped == true)
            {
                // have enemy death object face the same direction as the original enemy object
                enemyDeath.GetComponent<SpriteRenderer>().flipX = true;
            }

            TriggerTurret(enemyDeath);

            // add tokens
            tokensText.AddTokens(activeWord.hiraganaLength);

            // decrement enemyCount for waveSpawner
            WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
            waveSpawner.enemyCount--;

            // set active word boolean to false as the word has been completed and delete the active word from words list
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

    /// <summary>
    /// Makes a turret shoot a bullet after a hiragana has been removed
    /// </summary>
    public void TriggerTurret(GameObject enemyGameObject)
    {   
        // trigger turret animation and play sound effect
        turrets[turretIndex].SetTrigger("Shoot");
        GlobalAudioManager.instance.Play("Turret Shoot");

        // reference the active word's gameObject
        Transform enemyTarget = enemyGameObject.transform;
        turrets[turretIndex].GetComponentInParent<Turret>().RotateTurret(enemyTarget);

        // spawn bullet instance and give target gameObject
        GameObject bullet = Instantiate(turrets[turretIndex].GetComponentInParent<Turret>().bulletPrefab, turrets[turretIndex].transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().enemy = enemyTarget;

        // reset turretIndex to 0 once we are at the last index so that we don't go over bounds
        if (turretIndex + 1 >= turrets.Length)
            turretIndex = 0;
        else
            turretIndex++;  // make next turret shoot 
    }
}
