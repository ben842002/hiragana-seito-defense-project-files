using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokensCollected : MonoBehaviour
{
    TMP_Text tokensText;

    // Start is called before the first frame update
    void Start()
    {
        tokensText = GetComponent<TMP_Text>();
        tokensText.text = "Tokens Collected: " + PlayerStats.instance.tokensPerLevel + "/" + GameMaster.maxTokensToBeCollected;
    }
}
