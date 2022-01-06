using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // Constantly update this as you expand the game

    private void Start()
    {
        LoadPlayerStatsData();

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
            // NOTE: Constantly update this as you expand
            PlayerStats stats = PlayerStats.instance;
            stats.totalTokens = data.totalTokens;
            stats.maxLives = data.maxLives;

            Debug.Log("Loaded Player Stats from: " + path);
        }
        else
            Debug.LogError("File not found!");
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
