using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("Lives")]   // might have to move current lives to GameMaster 
    public int maxLives;
    [SerializeField] int _currentLives;
    public int currentLives // lives are initialized in Player.cs
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
