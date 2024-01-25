using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    public static int currentCoins;

    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        coinText.text = currentCoins.ToString();
    }
}
