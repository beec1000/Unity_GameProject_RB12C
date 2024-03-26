using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.GetHP(playerController.maxHealth);
            gameObject.SetActive(false);
        }
    }
}
