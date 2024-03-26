using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       playerController = collision.GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.GetHP(playerController.maxHealth);
            gameObject.SetActive(false);
        }
    }
}
