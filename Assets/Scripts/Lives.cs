using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    public static Lives instance;

    [SerializeField] TMP_Text livesText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // wait a second frame so that PlayerStatsData can be loaded in the Start method in DataManager.cs
    IEnumerator Start()
    {
        yield return null;
        livesText.text = PlayerStats.instance.maxLives.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = PlayerStats.instance.currentLives.ToString();
    }
}
