using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wsInitialCode : MonoBehaviour
{
    // WAVE CLASS --------------------------------------------------
    [System.Serializable]
    public class Wave
    {
        public Transform[] spawnPositions;

        public int enemyCount;
        public float rate;

        [Header("Words: Make sure array sizes are identical")]
        public GameObject[] enemyPrefab;
        public string[] romaji;
        public string[] hiragana;
    }
    // -------------------------------------------------------------
    public enum SpawnState
    {
        Spawning, Waiting, Counting
    };

    WordManager wordManager;
    WordInput wordInput;

    [Header("Start Waves")]
    [SerializeField] bool StartWaves;
    [SerializeField] Animator dialogueBoxAnim;

    [Header("Waves Count")]
    [SerializeField] TMP_Text wavesCountText;
    int wavesCount;

    [Header("Waiting Time Between Waves")]
    [SerializeField] float timeIntervalBetweenWaves;
    private float waveCountdown;

    [SerializeField] Wave[] waves;

    private int waveIndex = 0;
    private SpawnState state = SpawnState.Counting;

    // check if all enemies are killed in scene
    private float enemyAliveCheck = 0;

    private void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
        wordInput = FindObjectOfType<WordInput>();

        // initialize wave count and text
        UpdateWaveCounter(0);

        dialogueBoxAnim.SetBool("isOpen", true);
    }

    private void Update()
    {
        if (StartWaves == true)
        {
            if (state == SpawnState.Waiting)
            {
                // check if there are enemies that are alive
                if (EnemyAlive() == false)
                {
                    // begin new round
                    BeginNewWave();
                }
                else return;
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.Spawning)
                {
                    // start spawning a wave
                    StartCoroutine(SpawnWave(waves[waveIndex]));
                }
            }
            else
                waveCountdown -= Time.deltaTime;
        }
    }

    void BeginNewWave()
    {
        state = SpawnState.Counting;
        waveCountdown = timeIntervalBetweenWaves;

        // if the player completes the LAST wave, show victory screen
        if (waveIndex + 1 > waves.Length - 1)
        {
            GameMaster.gm.Victory();
            this.enabled = false;
        }
        else
        {
            waveIndex++;

            // increment wave counter 
            UpdateWaveCounter(++wavesCount);
        }
    }

    bool EnemyAlive()
    {
        enemyAliveCheck -= Time.deltaTime;
        if (enemyAliveCheck <= 0)
        {
            enemyAliveCheck = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        // update enum state to spawning
        state = SpawnState.Spawning;

        // Spawn enemies on an interval (_wave.rate)
        for (int i = 0; i < _wave.enemyCount; i++)
        {
            int randomPrefabIndex = Random.Range(0, _wave.enemyPrefab.Length);
            int randomIndex = Random.Range(0, _wave.spawnPositions.Length);
            wordManager.SpawnEnemyWord(_wave.enemyPrefab[randomPrefabIndex], _wave.spawnPositions[randomIndex].position, _wave.romaji[i], _wave.hiragana[i]);
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