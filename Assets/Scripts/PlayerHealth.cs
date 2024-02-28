using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private GameObject bloodDropsFX;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
