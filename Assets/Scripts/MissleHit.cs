using UnityEngine;

public class MissleHit : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject explosionEcffect;

    private ProjectileController controller;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        controller = GetComponentInParent<ProjectileController>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("Ground") || target.CompareTag("TrainingDummy"))
        {
            controller.Stop();
            Instantiate(explosionEcffect, transform.position, transform.rotation);
            Destroy(gameObject);      
        }

        if (target.CompareTag("Enemy"))
        {
            enemyHealth = target.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            controller.Stop();
            Instantiate(explosionEcffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
