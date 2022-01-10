using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    // this class is responsible for registering item buys and updating currency/item amounts

    [SerializeField] TMP_Text tokensText;

    [Header("Item Buy Values")]
    [SerializeField] int maxLivesIncrease;

    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.instance;

        // load PlayerStats when entering scene
        DataManager.LoadPlayerStatsData();
        tokensText.text = "Tokens: " + stats.totalTokens;
    }

    // --------------------------------------------------------
    // These functions are attached to button's onClick event

    public void BuyTimeSlow(int cost)
    {
        if (stats.totalTokens >= cost)
        {
            stats.totalTokens -= cost;
            tokensText.text = "Tokens: " + stats.totalTokens;

            stats.slowDownTime5s++;

            DataManager.SavePlayerStatsData(stats);
        }
        else
        {
            // play error sound here
        }
    }

    public void BuyMaxLives(int cost)
    {
        if (stats.totalTokens >= cost)
        {
            stats.totalTokens -= cost;
            tokensText.text = "Tokens: " + stats.totalTokens;

            stats.maxLives += maxLivesIncrease;

            DataManager.SavePlayerStatsData(stats);
        }
        else
        {
            // error sound
        }
    }
}
