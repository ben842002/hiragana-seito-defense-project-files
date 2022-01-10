using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    PlayerStats stats;
    [SerializeField] WordManager wordManager;

    [Header("Cinemachine Cameras")]
    [SerializeField] float camIntensity;
    [SerializeField] float camTime;

    [Header("Game Over Screen")]
    public GameObject gameOverUI;

    [Header("Victory Level Screen")]
    public GameObject victoryUI;

    int finishedLevel;

    [Header("Reset Level PlayerPrefs")]
    [SerializeField] bool resetLevelPlayerPrefs;

    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    private void Start()
    {
        // TO RESET levelReached PLAYERPREFS for this particular level
        if (resetLevelPlayerPrefs == true)
        {   
            // resets ALL progress made in Menu Selection (first level should be the only playable one)
            PlayerPrefs.SetInt("levelReached", 1);

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 0);
            Debug.Log("Resetted this Level's PlayerPrefs");
        }

        // -------------------------------------------------------------

        stats = PlayerStats.instance;

        // 0 = false | 1 = true
        finishedLevel = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0);
        if (finishedLevel == 0)
            print("Finished Level: False");
        else
            print("Finished Level: True");

        // ------------------------------------------------------------

        // instantiate lives
        stats.currentLives = stats.maxLives;

        // reset tokens every level (PlayerStats is DontDestroyOnLoad)
        stats.tokensPerLevel = 0;
    }

    // victory screen that is shown once player beats all levels
    public void Victory()
    {
        victoryUI.SetActive(true);
        GlobalAudioManager.instance.Play("Victory");

        // when player finishes a level, add the token amount collected this level to PlayerStats totalTokens variable and then save totalTokens to JSON.
        PlayerStats stats = PlayerStats.instance;
        stats.totalTokens += stats.tokensPerLevel;
        DataManager.SavePlayerStatsData(stats);

        // only increase levelValue once when player finishes the level for the first time
        if (finishedLevel == 0)
        {
            // allow player to play next level
            int levelValue = PlayerPrefs.GetInt("levelReached", 1);
            levelValue++;
            PlayerPrefs.SetInt("levelReached", levelValue);

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        }
    }

    // ----------------------------------------------------------------------
    // Functions called when an enemy makes it to the end of the path for a level
    public void RemoveLives(int livesRemoved, GameObject enemyGameObject)
    {   
        // subtract lives and update the text UI
        stats.currentLives -= livesRemoved;
        Lives.instance.UpdateLives();

        // camera shake and audio effect
        CinemachineShake.instance.ShakeCamera(camIntensity, camTime);
        GlobalAudioManager.instance.Play("Lose Lives");

        // check if enemy's word is the active word
        Word enemysWord = enemyGameObject.GetComponentInChildren<WordDisplay>().enemyWord;
        if (enemysWord == wordManager.activeWord)        
            wordManager.hasActiveWord = false;

        // remove enemy's word from Words list in WordManager
        wordManager.words.Remove(enemysWord);

        // check if lives <= 0. If it is, game over
        if (stats.currentLives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        // pause game and show game over menu
        PauseMenu.Pause();
        gameOverUI.SetActive(true);

        // sound effect
        GlobalAudioManager.instance.Play("Game Over");
    }   
}
