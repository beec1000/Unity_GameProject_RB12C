using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1;
    private bool pickedUp;

    private void Awake()
    {
        pickedUp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinCounter.Instance.ChangeScore(coinValue);
            pickedUp = true;
        }
    }
}
