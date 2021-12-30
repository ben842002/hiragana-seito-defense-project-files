using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour
{   
    // The purpose of this script is to show the correct amount of playable levels while disabling any levels that haven't been reach yet

    [Header("Level Buttons")]
    [SerializeField] Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Show available and locked levels
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelReached = PlayerPrefs.GetInt("levelReached", 1);

            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Animator>().SetBool("Disabled", true);
            }
        }
    }
}
