using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    PlayerStats stats;

    [Header("Total number of hearts on screen")]
    [SerializeField] int numOfHearts;

    [Header("Image Components")]
    [SerializeField] GameObject[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private void Start()
    {
        stats = PlayerStats.instance;
    }

    private void Update()
    {
        // if our max lives is more than the current number of hearts in scene (this could happen when player gets +1 for their max lives)
        // NOTE: This is capped at 6 for now (you have to manually add heart sprites to the hearts List
        if (stats.currentLives > numOfHearts)
            numOfHearts = stats.maxLives;

        for (int i = 0; i < hearts.Length; i++)
        {
            // display the same amount of full hearts as the amount in currentLives.
            // all other hearts will be shown as an empty heart because the player died
            Image image = hearts[i].GetComponent<Image>();
            if (i < stats.currentLives)
                image.sprite = fullHeart;
            else
                image.sprite = emptyHeart;

            // show the correct amount of TOTAL hearts on screen. 
            if (i < numOfHearts)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false); // this is so that the number of hearts displayed on screen doesnt exceed numOfHearts value
        }
    }
}
