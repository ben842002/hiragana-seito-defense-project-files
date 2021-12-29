using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera currentCam;

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

    // -------------------------------------------------------------------
    // Transitions
    public void TransitionCinemachineCamera(CinemachineVirtualCamera otherCamera)
    {   
        // readjust priority values
        currentCam.m_Priority = 10;
        otherCamera.m_Priority = 100;

        // update active camera reference
        currentCam = otherCamera;
    }

    public void Fade(Animator animator)
    {
        animator.SetTrigger("Fade");
    }
}
