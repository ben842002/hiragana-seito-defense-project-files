using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

        // ------------------------------------------------------------

        // instantiate lives
        stats.currentLives = stats.maxLives;
    }

    // victory screen that is shown once player beats all levels
    public void Victory()
    {
        // audio effect here
        victoryUI.SetActive(true);
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
