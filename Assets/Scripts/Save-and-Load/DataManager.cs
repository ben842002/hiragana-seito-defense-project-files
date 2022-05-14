using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class responsible for saving and loading PlayerStats data.
/// </summary>
public class DataManager : MonoBehaviour
{
    private void Start()
    {   
        // At the start of each level, load in all saved data
        CheckForSaveFiles();

        // FOR TESTING PURPOSES ONLY: Delete PlayerStats save file
        //DeleteSaveFile("/playerStats.json");
    }

    // --------------------------------------------------
    // PlayerStats

    /// <summary>
    /// Saves values stored in PlayerStats to a JSON file. 
    /// </summary>
    public static void SavePlayerStatsData(PlayerStats stats)
    {   
        // create a PlayerStatsData object that contains all savable PlayerStats values and pass the object in for JSON serializing
        PlayerStatsData data = new PlayerStatsData(stats);
        string json = JsonUtility.ToJson(data);

        // write the produced JSON string to a file so that it can be read later (aka loading data)
        string path = Application.persistentDataPath + "/playerStats.json";
        File.WriteAllText(path, json);

        Debug.Log("Saved Player Stats to: " + path);
    }

    public static void LoadPlayerStatsData()
    {
        string path = Application.persistentDataPath + "/playerStats.json";

        if (File.Exists(path) == true)
        {   
            // read from JSON file and convert the string back to an object with values
            string json = File.ReadAllText(path);
            PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(json);

            // initialize PlayerStats instance values to equal the values of the PlayerStatsData object
            PlayerStats stats = PlayerStats.instance;
            LoadStats(stats, data);

            Debug.Log("Loaded Player Stats from: " + path);
        }
        else
            Debug.LogError("File not found!");
    }

    /// <summary>
    /// Static function that assigns all PlayerStats variables. This function will be updated constantly
    /// </summary>
    /// <param name="stats"></param>
    /// <param name="data"></param>
    private static void LoadStats(PlayerStats stats, PlayerStatsData data)
    {
        stats.totalTokens = data.totalTokens;
        stats.maxLives = data.maxLives;

        stats.slowDownTime5s = data.slowDownTime5s;
        stats.enemyDestroyers = data.enemyDestroyers;
    }

    // -------------------------------------------------
    public static void CheckForSaveFiles()
    {
        string jsonPath = Application.persistentDataPath + "/playerStats.json";
        if (File.Exists(jsonPath) == true)
            LoadPlayerStatsData();
        else
            Debug.LogError("File does not exist: " + jsonPath);
    }

    // -------------------------------------------------
    // Delete Save File

    /// <summary>
    /// Deletes JSON save file if found. For the paramter, pass in the concatenated string portion of the path.
    /// Example: DeleteSaveFile("/playerStats.json").
    /// </summary>
    public static void DeleteSaveFile(string fileName)
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path) == true)
        {
            File.Delete(path);
            Debug.Log("Deleted: " + path);
            return;
        }

        Debug.LogError("File not found!");
    }
}
