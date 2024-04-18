using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float dummyHealth = float.PositiveInfinity;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        if (gameObject.CompareTag("TrainingDummy")) currentHealth = dummyHealth;
        if (gameObject.CompareTag("Enemy")) currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            MakeDeath();
        }
    }

    private void MakeDeath()
    {
        Destroy(transform.parent.gameObject);
    }
}
