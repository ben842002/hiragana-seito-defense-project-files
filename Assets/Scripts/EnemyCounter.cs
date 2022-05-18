using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter instance;

    TMP_Text enemyCounter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = GetComponentInChildren<TMP_Text>();

        // initialize text
        WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
        UpdateEnemyCounter(waveSpawner.enemyCount);
    }

    public void UpdateEnemyCounter(int amount)
    {
        enemyCounter.text = ": " + amount;
    }
}
