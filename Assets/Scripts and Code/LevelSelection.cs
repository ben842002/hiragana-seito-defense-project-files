using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject levelText;
    [SerializeField] bool isInRange;

    [Header("Level Name")]
    [SerializeField] string levelName;

    private void Update()
    {   
        // check if player presses E while in range 
        if (isInRange == true && Input.GetKeyDown(KeyCode.E))
            StartCoroutine(LevelLoader.instance.LoadLevelByString(levelName));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            // When player is in range, check for player input in the Update function and display text UI on screen 
            // (trigger opposite when player isn't in range)
            levelText.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelText.SetActive(false);
            isInRange = false;
        }
    }
}
