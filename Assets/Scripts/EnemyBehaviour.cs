using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] private float flipTime = 2f;

    private Rigidbody2D rigidbody2d;

    private bool facingRight = false;
    private bool canFlip = true;

    private float nextFlip = 0f;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canFlip && Time.time > nextFlip)
        {
            if (Random.Range(0, 10) >= 5) FlipFacing();
            nextFlip = Time.time + flipTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (facingRight != collision.transform.position.x > transform.position.x)
            {
                FlipFacing();
            }
            canFlip = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rigidbody2d.AddForce(new Vector2(x: facingRight ? +1 : -1, y: 0) * runSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canFlip = true;

            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void FlipFacing()
    {
        facingRight = !facingRight;
        transform.localScale =
            new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
    }
}
