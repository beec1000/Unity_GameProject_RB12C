using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;
    public TextMeshProUGUI text;
    public int score;

    private void Start()
    {
        if (Instance == null) 
        { 
            Instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = score.ToString();
    }
}
