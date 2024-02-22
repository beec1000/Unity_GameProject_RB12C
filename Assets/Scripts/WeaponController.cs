using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool HandsFree;

    void Start()
    {
        HandsFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent = collision.transform;
            HandsFree = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
