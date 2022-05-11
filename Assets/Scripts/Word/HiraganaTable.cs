using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads in a text file containing all romaji-to-hiragana rules into a Dictionary which is used when checking if the player correctly typed a hiragana.
/// </summary>
public class HiraganaTable : MonoBehaviour
{
    public static Dictionary<string, int> hiraganaTable = new();

    [SerializeField] TextAsset hiraganaTableFile;

    // Start is called before the first frame update
    void Start()
    {
        if (hiraganaTableFile == null)
            Debug.LogError("Hiragana file not found!");

        ReadFile();
    }

    void ReadFile()
    {
        // handle any possibilities for ending of each line
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        var splitLine = new char[] { ',' };

        // retrieve all lines from file. Remove spaces when doing so
        var lines = hiraganaTableFile.text.Split(splitFile, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            //print("Line: " + lines[i]);

            // for each line, split using the delimiters specified in splitLine. Store romaji and number of hiragana into dictionary
            var line = lines[i].Split(splitLine, System.StringSplitOptions.None);
            string romaji = line[0];
            int numberOfHiragana = int.Parse(line[1]);
            hiraganaTable.Add(romaji, numberOfHiragana);
        }
    }
}
