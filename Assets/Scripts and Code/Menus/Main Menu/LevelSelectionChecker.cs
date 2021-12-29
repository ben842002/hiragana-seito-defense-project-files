using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelSelectionChecker : MonoBehaviour
{
    // The purpose of this script is to check whether we are loading the Level Selection initially 

    [Header("Cinemachine Cameras")]
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera levelSelectionCamera;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectionMenu;

    // Start is called before the first frame update
    void OnLevelWasLoaded()
    {
        if (LoadLevelSelection.instance.loadLevelSelection == true)
        {
            // adjust camera priorities
            mainCamera.m_Priority = 1;
            levelSelectionCamera.m_Priority = 10;

            // set level selection menu gameObject to active
            mainMenu.SetActive(false);
            levelSelectionMenu.SetActive(true);

            LoadLevelSelection.instance.loadLevelSelection = false;
        }
    }
}
