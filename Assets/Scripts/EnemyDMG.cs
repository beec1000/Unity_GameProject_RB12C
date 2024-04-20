using UnityEngine;

public class EnemyDMG : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float damageDelay = 1f;
    [SerializeField] private float damage = 2f;

    private PlayerController playerController;
    private bool isPlayerTouching = false;
    private bool canDamage = true;
    private float lastDamageTime = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                isPlayerTouching = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (isPlayerTouching && canDamage)
        {
            playerController = player.GetComponent<PlayerController>();

            if (playerController != null && !playerController.isGrounded && playerController.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                PushBack(player.transform);
            }

            if (Time.time - lastDamageTime >= damageDelay)
            {
                playerController.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerTouching = false;
            playerController = null;
        }
    }

    private void PushBack(Transform playerTransform)
    {
        Rigidbody2D playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(transform.up * pushForce, ForceMode2D.Impulse);
        playerRigidbody.AddForce(transform.right * pushForce, ForceMode2D.Impulse);
    }
}
