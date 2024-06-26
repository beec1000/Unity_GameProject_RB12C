using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private AudioClip enemyDeathSound;

    private AudioSource audioS;
    private Animator animator;
    private float dummyHealth;
    private float currentHealth;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        dummyHealth = Mathf.Infinity;
        currentHealth = maxHealth;

        if (gameObject.CompareTag("TrainingDummy")) currentHealth = dummyHealth;
        if (gameObject.CompareTag("Enemy")) currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            MakeDeath();
        }
    }

    private void MakeDeath()
    {
        audioS.PlayOneShot(enemyDeathSound);
        Destroy(transform.parent.gameObject, 2f);
    }
}
