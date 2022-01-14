using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // This script is responsible for all inventory related logic such as updating text and defining item game behavior

    PlayerStats stats;

    [Header("Inventory GameObject")]
    [SerializeField] GameObject inventory;

    [Header("Text Objects")]
    [SerializeField] TMP_Text slowTimeText;

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
        slowTimeText.text = "SlowTime: " + stats.slowDownTime5s;
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

        // Slow Time
        if (Input.GetKeyDown(KeyCode.Alpha1) && stats.slowDownTime5s >= 1)
            ActivateSlowTime();
        
        if (isInTimeSlowAbility == true)
            SlowTime();
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
        slowTimeText.text = "SlowTime: " + stats.slowDownTime5s;   
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
}
