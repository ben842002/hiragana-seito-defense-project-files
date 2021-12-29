using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelection : MonoBehaviour
{
    public static LoadLevelSelection instance;

    // when the player finishes a level and presses continue, we want to load straight into the level selection scene
    public bool loadLevelSelection;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // persist through scenes
        DontDestroyOnLoad(gameObject);
    }
}
