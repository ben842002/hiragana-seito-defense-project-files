using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    // WAVE CLASS --------------------------------------------------
    [System.Serializable]
    public class Wave
    {
        public Transform[] spawnPositions;

        public float rate;

        [Header("Words: Make sure array sizes are identical")]
        public List<GameObject> enemyPrefab;
        public List<string> romaji;
        public List<string> hiragana;
    }
    // -------------------------------------------------------------

    WordManager wordManager;
    WordInput wordInput;

    public enum SpawnState 
    {
        Spawning,   // while enemies are spawning during a wave
        Waiting,    // while enemies are all spawned but still active in the scene
        Counting    // brief waiting time betwwen when a new wave starts and the first enemy spawning
    };

    [Header("Start Waves")]
    [SerializeField] Animator dialogueBoxAnim;
    bool StartWaves;

    TMP_Text wavesCountText;
    int wavesCount;

    [Header("Waiting Time Between Waves")]
    [SerializeField] float timeIntervalBetweenWaves;
    private float waveCountdown;

    [Header("Waypoints")]
    [SerializeField] Waypoints[] waypointObjects;

    public Wave[] waves;

    private int waveIndex = 0;
    private SpawnState state = SpawnState.Counting;

    // check if all enemies are killed in scene
    [HideInInspector] public int enemyCount;

    private void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
        wordInput = FindObjectOfType<WordInput>();
        wavesCountText = GameObject.FindGameObjectWithTag("WaveCounter").GetComponent<TMP_Text>();

        // initialize wave count and text
        UpdateWaveCounter(0);

        // Show dialogue box to player when entering scene
        dialogueBoxAnim.SetBool("isOpen", true);
    }

    private void Update()
    {
        if (StartWaves == true)
        {   
            // Check if all enemies are cleared during a wave. If so, begin a new one
            if (state == SpawnState.Waiting)
            {
                // check if there are enemies that are alive
                if (enemyCount == 0)
                {
                    // begin new round
                    BeginNewWave();
                }
                else return;
            }

            // Whenever a new wave begins, we have a brief interval of time to wait before enemies start spawning
            if (waveCountdown > 0)
            {
                // decrease timer
                waveCountdown -= Time.deltaTime;
            }
            else
            {
                if (state != SpawnState.Spawning)
                {
                    // start spawning a wave
                    StartCoroutine(SpawnWave(waves[waveIndex]));
                }
            }
        }
    }

    void BeginNewWave()
    {
        state = SpawnState.Counting;

        // initial waiting interval timer
        waveCountdown = timeIntervalBetweenWaves;

        // if the player completes the LAST wave, show victory screen
        if (waveIndex + 1 > waves.Length - 1)
        {
            GameMaster.gm.Invoke("Victory", 1.5f);

            enabled = false;    // disable script
        }
        else
        {
            waveIndex++;

            // increment wave counter 
            UpdateWaveCounter(++wavesCount);
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        // update enum state to spawning
        state = SpawnState.Spawning;

        // hiragana.Length and romaji.Length will always be the same which means you can use either or
        enemyCount = _wave.hiragana.Count;

        // Spawn enemies on an interval (_wave.rate)
        int loopAmount = _wave.hiragana.Count;
        for (int i = 0; i < loopAmount; i++)
        {
            // if there is only one element in the enemyPrefab array, then all enemies will be that particular prefab. If not, spawn enemy with the prefab that is in
            // the corresponding index value
            int prefabIndex = 0;
            if (_wave.enemyPrefab.Count != 1)
                prefabIndex = i;

            // generate random index for the spawn point and waypoint path of the enemy
            int randomSpawnIndex = Random.Range(0, _wave.spawnPositions.Length);

            // Choose a hiragana randomly for this particular wave
            int randomHiraganaIndex = Random.Range(0, _wave.hiragana.Count);

            // -----------------------------------------------
            // spawn enemy at a location with its word
            GameObject enemyPrefab = _wave.enemyPrefab[prefabIndex];
            Vector2 spawnPos = _wave.spawnPositions[randomSpawnIndex].position;
            string romaji = _wave.romaji[randomHiraganaIndex];
            string hiragana = _wave.hiragana[randomHiraganaIndex];
            Waypoints waypoints = waypointObjects[randomSpawnIndex];
            wordManager.SpawnEnemyWord(enemyPrefab, spawnPos, romaji, hiragana, waypoints);

            // remove hiragana and romaji so that it is not considered when choosing a random index
            _wave.romaji.Remove(_wave.romaji[randomHiraganaIndex]);
            _wave.hiragana.Remove(_wave.hiragana[randomHiraganaIndex]);

            yield return new WaitForSeconds(_wave.rate);
        }

        state = SpawnState.Waiting;
    }

    // ------------------------------
    // button and text function
    public void StartWaveSpawn()
    {
        Invoke(nameof(StartBool), .25f);
        wordInput.enabled = true;
        dialogueBoxAnim.SetBool("isOpen", false);
        UpdateWaveCounter(1);
    }

    /// <summary>
    /// Starts the wave sequence
    /// </summary>
    void StartBool()
    {
        StartWaves = true;
    }

    void UpdateWaveCounter(int waveNumber)
    {
        wavesCount = waveNumber;
        wavesCountText.text = "Wave: " + wavesCount + "/" + waves.Length;
    }
}
