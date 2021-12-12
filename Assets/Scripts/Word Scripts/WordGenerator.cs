using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // input/output
using System.Linq;

public class WordGenerator : MonoBehaviour
{
    private static string[] originalList;

    // these are hidden from inspector for now since we aren't using it atm
    [SerializeField] public List<string> hiraganaList;
    [SerializeField] public List<string> romajiList;

    string filePath;
    
    [Header("File Name: IMPORTANT")]
    [SerializeField] private string fileName;

    [Header("Toggle whether txt file is in Editor or Build: VERY IMPORTANT")]
    [SerializeField] bool editorTXT;
    
    //private void Start()
    //{   
    //    if (editorTXT == true)
    //    {
    //        // for EDITOR
    //        filePath = Application.dataPath + "/" + fileName;   
    //    }
    //    else
    //    {
    //        // for BUILD
    //        filePath = fileName;                             
    //    }

    //    ReadFromFile();
    //}

    // you can put any addition functionalities here such as sorting by alphabetical order or by length of word
    void ReadFromFile()
    {
        // each line in file will be stored as a string element in originalList
        originalList = File.ReadAllLines(filePath);

        // parse each individual string in originalList, adding hiragana to one and romaji to another
        for (int i = 0; i < originalList.Length; i++)
        {   
            // separate using a comma as our condition
            string[] splitLine = originalList[i].Split(',');

            // append hiragana to list
            string hiragana = splitLine[0];
            hiraganaList.Add(hiragana);

            // remove whitespace in front of first letter before appending to list
            string romaji = splitLine[1].Trim();
            romajiList.Add(romaji);
        }
    }

    public int GetRandomWordIndex()
    {
        int randomIndex = Random.Range(0, hiraganaList.Count);
        return randomIndex;
    }
}
