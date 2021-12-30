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

    [Header("Text Fonts")]
    [SerializeField] Font[] font;

    [Header("Cinemachine Cameras")]
    [SerializeField] float camIntensity;
    [SerializeField] float camTime;

    [Header("Game Over Screen")]
    public GameObject gameOverUI;

    [Header("Victory Level Screen")]
    public GameObject victoryUI;

    int finishedLevel;

    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    private void Start()
    {
        stats = PlayerStats.instance;

        // make font pixel perfect
        for (int i = 0; i < font.Length; i++)
            font[i].material.mainTexture.filterMode = FilterMode.Point;

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
        stats.tokens = 0;


        // TO RESET PLAYERPREFS for this particular level
        //PlayerPrefs.SetInt("levelReached", 1);
        //PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 0);
    }

    // victory screen that is shown once player beats all levels
    public void Victory()
    {
        // audio effect here
        victoryUI.SetActive(true);

        // only increase levelValue when player finishes the level for the first time
        if (finishedLevel == 0)
        {
            // allow player to play next level
            int levelValue = PlayerPrefs.GetInt("levelReached");
            levelValue++;
            print(levelValue);
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

        // camera shake effect
        CinemachineShake.instance.ShakeCamera(camIntensity, camTime);

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

        // add audio here
    }   
}
