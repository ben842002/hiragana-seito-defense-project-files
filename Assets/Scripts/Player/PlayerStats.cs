using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("Tokens")]
    public int totalTokens;
    public int tokensPerLevel;

    [Header("Lives")] 
    public int maxLives;
    [SerializeField] int _currentLives;

    [Header("Activatables")]
    public int slowDownTime5s;
    public int enemyDestroyers;

    public int currentLives // lives are initialized in GameMaster.cs
    {
        get { return _currentLives; }
        set { _currentLives = Mathf.Clamp(value, 0, maxLives); }
    }

    [Header("Movement")]
    public float moveSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // have PlayerStats gameObject persist through scenes
        DontDestroyOnLoad(gameObject);
    }
}
