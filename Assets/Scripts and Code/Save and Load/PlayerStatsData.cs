using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsData
{
    public int totalTokens;
    public int maxLives;

    /// <summary>
    /// Constructor that takes in the PlayerStats instance. Make sure to constantly keep this constructor up-to-date by 
    /// making sure the savable variables in PlayerStatsData.cs are identical to the ones in PlayerStats.cs.
    /// </summary>
    /// <param name="stats"></param>
    public PlayerStatsData(PlayerStats stats)
    {
        totalTokens = stats.totalTokens;
        maxLives = stats.maxLives;
    }
}
