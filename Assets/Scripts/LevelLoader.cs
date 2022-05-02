using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] Animator animator;
    [SerializeField] float transitionDelay;
    readonly string Transition = "Transition";

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    /// <summary>
    /// Load level by scene index (integer)
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public IEnumerator LoadLevelByIndex(int levelIndex)
    {
        animator.SetTrigger(Transition);
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Load level by scene name (string)
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    public IEnumerator LoadLevelByName(string levelName)
    {
        animator.SetTrigger(Transition);
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Special function for loading a scene from a paused game state (Ex: PAUSE MENU, GAME OVER, VICTORY SCREEN)
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public IEnumerator LoadLevelByIndexFromPause(int levelIndex)
    {
        animator.SetTrigger(Transition);
        yield return new WaitForSecondsRealtime(transitionDelay);
        PauseMenu.Resume();
        SceneManager.LoadScene(levelIndex);
    }
}
