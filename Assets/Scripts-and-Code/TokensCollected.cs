using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokensCollected : MonoBehaviour
{
    WaveSpawner waveSpawner;

    TMP_Text tokensText;
    int maxScore;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = GameMaster.gm.GetComponent<WaveSpawner>();
        tokensText = GetComponent<TMP_Text>();

        // loop through each wave element in the Waves array in WaveSpawner.cs
        for (int i = 0; i < waveSpawner.waves.Length; i++)
        {   
            // loop through the hiragana list, determining the max score a player can get
            for (int j = 0; j < waveSpawner.waves[i].hiragana.Length; j++)
            {
                maxScore += waveSpawner.waves[i].hiragana[j].Length;
            }
        }

        tokensText.text = "Tokens Collected: " + PlayerStats.instance.tokensPerLevel + "/" + maxScore;
    }
}
