using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsible for registering item buys and updating currency/item amounts
/// </summary>
public class ShopMenu : MonoBehaviour
{
    [SerializeField] TMP_Text tokensText;

    [Header("Item Buy Values")]
    [SerializeField] int maxLivesIncrease;

    PlayerStats stats;
    GlobalAudioManager GAM;

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.instance;
        GAM = GlobalAudioManager.instance;

        // load PlayerStats when entering scene
        DataManager.CheckForSaveFiles();

        tokensText.text = "Tokens: " + stats.totalTokens;
    }

    // --------------------------------------------------------
    // These functions are attached to button's onClick event

    void UpdateShopInfo(int cost)
    {
        stats.totalTokens -= cost;
        tokensText.text = "Tokens: " + stats.totalTokens;
        GAM.Play("Buy Success");
    }

    public void BuyTimeSlow(int cost)
    {
        if (stats.totalTokens >= cost)
        {
            UpdateShopInfo(cost);
            stats.slowDownTime5s++;
            DataManager.SavePlayerStatsData(stats);
        }
        else
            GAM.Play("Buy Failure");       
    }

    public void BuyMaxLives(int cost)
    {
        if (stats.totalTokens >= cost)
        {
            UpdateShopInfo(cost);
            stats.maxLives += maxLivesIncrease;
            DataManager.SavePlayerStatsData(stats);
        }
        else       
            GAM.Play("Buy Failure");       
    }

    public void BuyEnemyDestroyer(int cost)
    {
        if (stats.totalTokens >= cost)
        {
            UpdateShopInfo(cost);
            stats.enemyDestroyers++;
            DataManager.SavePlayerStatsData(stats);
        }
        else
            GAM.Play("Buy Failure");
    }
}
