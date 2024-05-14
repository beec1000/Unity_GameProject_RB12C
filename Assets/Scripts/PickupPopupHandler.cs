using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPopupHandler : MonoBehaviour
{
    [SerializeField] private GameObject pickupText;

    void Start()
    {
        pickupText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickupText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickupText.SetActive(false);
        }
    }

}
