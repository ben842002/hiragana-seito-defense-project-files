using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is responsible for all inventory related logic such as updating text and defining item game behavior.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    PlayerStats stats;

    [Header("Inventory GameObject")]
    [SerializeField] GameObject inventory;

    [Header("Text Objects")]
    [SerializeField] TMP_Text slowTimeText;
    [SerializeField] TMP_Text enemyDestroyerText;

    [Header("Slow Time Ability")]
    public float timeScaleSlowedAmount;
    [SerializeField] float slowTimeCountdown;
    float slowTimer;
    
    public static bool isInTimeSlowAbility; // this bool is used in PauseMenu.cs 

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        stats = PlayerStats.instance;

        // initialize text
        slowTimeText.text = "1. SlowTime: " + stats.slowDownTime5s;
        enemyDestroyerText.text = "2. EnemyDestroyer: " + stats.enemyDestroyers;
    }

    // Update is called once per frame
    void Update()
    {   
        // When player presses TAB, open/close the inventory
        if (Input.GetKeyDown(KeyCode.Tab) && inventory.activeSelf == false)
        {
            inventory.SetActive(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Tab) && inventory.activeSelf == true)
        {
            inventory.SetActive(false);
        }

        // ----------------------------------------------------
        // USER INPUT TO ACTIVATE ITEMS/CONSUMABLES

        // -------------
        // Slow Time
        if (Input.GetKeyDown(KeyCode.Alpha1) && stats.slowDownTime5s >= 0)
            ActivateSlowTime();
        
        if (isInTimeSlowAbility == true)
            SlowTime();

        // -------------
        // Enemy Destroyer
        if (Input.GetKeyDown(KeyCode.Alpha2) && stats.enemyDestroyers > 0)
            ActivateEnemyDestroyer();
    }

    // -------------------------------------------------------------------
    // Slow Time
    void ActivateSlowTime()
    {   
        // decrement consumable amount
        stats.slowDownTime5s--;

        isInTimeSlowAbility = true;

        // initial timers
        slowTimer = slowTimeCountdown;
        Time.timeScale = timeScaleSlowedAmount;
        
        // update text
        slowTimeText.text = "1. SlowTime: " + stats.slowDownTime5s;   
    }

    void SlowTime()
    {   
        if (slowTimer <= 0)
        {
            Time.timeScale = 1f;
            isInTimeSlowAbility = false;
        } 
        // 2 Things to Note:
        // First is that we are using unscaledDeltaTime so that we are counting down the 5 seconds in realtime. 
        // Second is that we only want to count down when the game is not paused (the timer will keep decreasing while in pause if we don't add the if-statement)
        else if (PauseMenu.GameIsPaused == false)  
            slowTimer -= Time.unscaledDeltaTime;             
    }

    // ------------------------------------------------------------------
    // Enemy Destroyer

    void ActivateEnemyDestroyer()
    {
        stats.enemyDestroyers--;

        // get all enemies in the scene and kill them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            // access WordDisplay component to destroy enemy
            WordDisplay wd = enemies[i].GetComponentInChildren<WordDisplay>();
            wd.RemoveWord();
        }
    }
}
