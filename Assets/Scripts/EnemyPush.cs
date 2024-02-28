using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float pushForce = 20f;
    //private void OnTriggerStay2D(Collider2D player)
    //{
    //    PlayerController playerController = player.GetComponent<PlayerController>();
    //    if (player.CompareTag("Player") && playerController != null && !playerController.IsGrounded())
    //    {
    //        PushBack(player.transform);
    //    }
    //}

    private void OnTriggerStay2D(Collider2D player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

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
    }
}
