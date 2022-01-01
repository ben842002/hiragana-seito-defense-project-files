using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{   
    // Note: VictoryUI also uses this script because I didn't realize at the time that they would share the same button functionalities
    // These functions are called via clicking on a certain button

    public void Restart()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LevelLoader.instance.LoadLevelByIndexFromPause(levelIndex));
    }

    public void Menu()
    {
        StartCoroutine(LevelLoader.instance.LoadLevelByIndexFromPause(0));
    }

    public void QuitGame()
    {
        Debug.Log("You quit");
        Application.Quit();
    }

    public void ReturnToLevelSelection()
    {
        LoadLevelSelection.instance.loadLevelSelection = true;
        StartCoroutine(LevelLoader.instance.LoadLevelByIndexFromPause(0));
    }

    public void HoverSound()
    {
        GlobalAudioManager.instance.Play("Button Hover");
    }
}
