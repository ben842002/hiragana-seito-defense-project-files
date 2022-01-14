using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemStatValues : MonoBehaviour
{
    [SerializeField] TMP_Text slowTimeText;
    [SerializeField] TMP_Text maxLivesText;

    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.instance;
    }

    // Update is called once per frame
    void Update()
    {
        slowTimeText.text = "Slow Time: " + stats.slowDownTime5s;
        maxLivesText.text = "Max Lives: " + stats.maxLives;
    }
}
