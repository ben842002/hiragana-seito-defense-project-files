using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{   
    /// <summary>
    /// Instantiates an enemy prefab game object at a spawn position. Then access its wordDisplay component and returns it.
    /// </summary>
    public WordDisplay SpawnWordDisplay(GameObject enemyPrefab, Vector2 spawnPos)
    {
        GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        WordDisplay wordDisplay = enemyGameObject.GetComponentInChildren<WordDisplay>();
        return wordDisplay;
    }
}
