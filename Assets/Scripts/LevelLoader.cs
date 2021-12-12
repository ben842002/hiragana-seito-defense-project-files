using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] Animator animator;
    [SerializeField] float transitionDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public IEnumerator LoadLevelByIndex(int levelIndex)
    {
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator LoadLevelByString(string levelName)
    {
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(levelName);
    }

    // special function for loading a scene from a paused game state (ex: PAUSE MENU, GAME OVER, VICTORY SCREEN)
    public IEnumerator LoadLevelByIndexFromPause(int levelIndex)
    {
        animator.SetTrigger("Transition");
        yield return new WaitForSecondsRealtime(transitionDelay);
        PauseMenu.Resume();
        SceneManager.LoadScene(levelIndex);
    }
}
