using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    // Video to reference by Brackeys: https://www.youtube.com/watch?v=-cTgL9jhpUQ

    public void Select(int levelIndex)
    {
        StartCoroutine(LevelLoader.instance.LoadLevelByIndex(levelIndex));
    }
}
