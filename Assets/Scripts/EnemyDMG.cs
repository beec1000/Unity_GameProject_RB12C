using UnityEngine;

public class EnemyDMG : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;

    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.TakeDamage(1f);
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        playerController = player.GetComponent<PlayerController>();

        if (playerController != null && !playerController.isGrounded && playerController.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            PushBack(player.transform);
        }
    }

    private void PushBack(Transform playerTransform)
    {
        Rigidbody2D playerRigidbody = playerTransform.gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(transform.up * pushForce, ForceMode2D.Impulse);
        playerRigidbody.AddForce(transform.right * pushForce, ForceMode2D.Impulse);
    }
}
