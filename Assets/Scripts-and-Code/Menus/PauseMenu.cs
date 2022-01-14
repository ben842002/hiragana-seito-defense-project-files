using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // main bool to check if game is paused or not 
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject settingsUI;

    GameMaster gm;

    private void Start()
    {
        gm = GameMaster.gm;
    }

    // Update is called once per frame
    void Update()
    {   
        // there is weird logic when more than one UI is active. To avoid that, only show pause menu when other UIs are off
        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameOverUI.activeSelf == false && gm.victoryUI.activeSelf == false)
        {
            // if the game is paused and player presses ESC, resume game. If not, pause the game and display pause menu
            if (GameIsPaused == true)         
                ResumeGame();
            else
                DisplayPauseMenu();
        }
    }

    // ----------------------------
    // STATIC FUNCTIONS (they can be used in other scripts)
    public static void Pause()
    {
        Time.timeScale = 0f; // freeze time/game
        GameIsPaused = true;
    }

    public static void Resume()
    {   
        // We have to account for when the player unpauses when a time slow ability is being used (ability code is located in InventoryManager.cs).
        // If they are not using it, resume back to normal speed (1f). If a time slow ability is being used, we want to set the timeScale back to the value that it was.
        if (InventoryManager.isInTimeSlowAbility == false) 
            Time.timeScale = 1f; 
        else
        {
            float slowTimeAbilityTimeScale = FindObjectOfType<InventoryManager>().timeScaleSlowedAmount;
            Time.timeScale = slowTimeAbilityTimeScale;
        }

        GameIsPaused = false;
    }

    // --------------------------

    public void ResumeGame()
    {   
        // When the player is in settings mode and presses esc, return to pause menu instead of unpausing game
        if (settingsUI.activeSelf == true)
        {
            pauseMenuUI.SetActive(true);
            settingsUI.SetActive(false);
        }
        // Unpause the game if player is in the pause menu
        else
        {
            pauseMenuUI.SetActive(false);
            Resume();
        }
    }

    public void DisplayPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Pause();
    }

    // --------------------------
    // Button functions
    public void Restart()
    {
        int currLevelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LevelLoader.instance.LoadLevelByIndexFromPause(currLevelIndex));
    }

    public void LoadMenu()
    {
        StartCoroutine(LevelLoader.instance.LoadLevelByIndexFromPause(0));   
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ButtonHover()
    {
        GlobalAudioManager.instance.Play("Button Hover");
    }
}
