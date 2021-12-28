using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LevelLoader.instance.LoadLevelByIndex(nextLevelIndex));
    }

    public void Quit()
    {
        Debug.Log("You quit the game!");
        Application.Quit();
    }
}
