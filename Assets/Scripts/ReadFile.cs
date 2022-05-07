using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for reading a text file specified in the Unity inspector.
/// </summary>
public class ReadFile : MonoBehaviour
{
    [SerializeField] TextAsset hiraganaFile;
    
    /// <summary>
    /// Read through the specified file for this level and parse through each line storing the romaji and hiragana word in two separate lists. When the parser
    /// hits a line that says: break, enqueue both lists in their respective Queues. These Queues are used when initializing the words for the waves.
    /// </summary>
    /// <param name="romajiLists"></param>
    /// <param name="hiraganaLists"></param>
    public void ReadTextFile(ref Queue<List<string>> romajiLists, ref Queue<List<string>> hiraganaLists)
    {   
        // handle any possibilities for ending of each line
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        var splitLine = new char[] { ',' };

        // retrieve all lines from file
        var lines = hiraganaFile.text.Split(splitFile, System.StringSplitOptions.None);

        List<string> romajiList = new List<string>();
        List<string> hiraganaList = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            //print("Line: " + lines[i]);

            // for each line, split using the delimiters specified in splitLine.
            var line = lines[i].Split(splitLine, System.StringSplitOptions.None);

            // continuously add the hiragana and romaji to their lists until a line contains a word: break. If so, enqueue the lists
            if (line[0] == "break")
            {   
                // enqueueing the two actual lists will result in all the waves' hiragana and romaji list to be the same. To avoid this, enqueue copy instances instead.
                List<string> romajiCopy = new List<string>(romajiList);
                List<string> hiraganaCopy = new List<string>(hiraganaList);

                // clear the lists' items so that enqueueing again will not include the previous words enqueued earlier
                romajiList.Clear();
                hiraganaList.Clear();

                romajiLists.Enqueue(romajiCopy);
                hiraganaLists.Enqueue(hiraganaCopy);
            }
            else
            {   
                string romaji = line[0];
                string hiragana = line[1];

                romajiList.Add(romaji);
                hiraganaList.Add(hiragana);
            }
        }
    }
}
