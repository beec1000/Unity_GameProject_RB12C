using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private GameObject bloodDropsFX;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float dmgSmoothTime = .5f;

    private float currentHealth;
    private bool getDamage;

    private void Start()
    {
        getDamage = false;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        getDamage = true;
        healthBar.value = currentHealth;
        Instantiate(bloodDropsFX, transform.position, transform.rotation);
        if (currentHealth <= 0)
        {
            MakeDead();
        }
    }

    private void MakeDead()
    {
        Destroy(gameObject);
    }
}
