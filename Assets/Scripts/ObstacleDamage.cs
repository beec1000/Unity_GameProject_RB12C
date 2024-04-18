using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float trapDamageInterval = 1f;

    private bool isInTrap = false;
    private float trapDamageTimer = 0f;
    private PlayerController playerController;

    private void Update()
    {
        if (isInTrap)
        {
            trapDamageTimer += Time.deltaTime;
            if (trapDamageTimer >= trapDamageInterval)
            {
                if (playerController != null)
                {
                    playerController.TakeDamage(2f);
                }
                trapDamageTimer = 0f;
            }
        }
        else
        {
            trapDamageTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrap = true;
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController == null)
            {
                playerController = collision.GetComponent<PlayerController>();
            }
            if (playerController != null && !playerController.isGrounded && playerController.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                PushBack(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrap = false;
            playerController = null;
        }
    }

    private void PushBack(Transform playerTransform)
    {
        Rigidbody2D playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(transform.up * pushForce, ForceMode2D.Impulse);
            playerRigidbody.AddForce(transform.right * pushForce, ForceMode2D.Impulse);
        }
    }
}
