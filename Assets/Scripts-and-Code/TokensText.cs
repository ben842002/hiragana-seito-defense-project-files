using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokensText : MonoBehaviour
{
    TMP_Text tokenText;

    // Start is called before the first frame update
    void Start()
    {
        tokenText = GetComponent<TMP_Text>();
    }

    public void AddTokens(int addAmount)
    {
        PlayerStats.instance.tokensPerLevel += addAmount;
        tokenText.text = "Tokens: " + PlayerStats.instance.tokensPerLevel;
    }
}
