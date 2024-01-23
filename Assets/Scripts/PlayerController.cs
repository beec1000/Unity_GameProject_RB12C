using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9;
    [SerializeField] private float jumpHeight = 650;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask secretLayer;

    [SerializeField] ParticleSystem dyingParticle;

    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private bool isFacingRight;
    private bool isGrounded;
    private bool isDead;

    private Vector2 startPos;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        isGrounded = false;
        dyingParticle.Pause();
        isDead = true;

        startPos = transform.position;
    }
    private void Update()
    {
        if (!isDead && rigidbody2d.position.y < -60)
        {
            Die();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            rigidbody2d.AddForce(new(0, jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));
        rigidbody2d.velocity = new(move * maxSpeed, rigidbody2d.velocity.y);
        if ((move < 0 && isFacingRight) || (move > 0 && !isFacingRight)) Flip();

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, .15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", rigidbody2d.velocity.y);

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }

    void Die()
    {
        isDead = true;
        dyingParticle.Play();
        StartCoroutine(Respawn(.6f));
    }

    void Disappear()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamagingObstacle")) 
        {   
            Die();
        }

        if (collision.CompareTag("Secret1"))
        {
            Disappear();
        }
    }

    System.Collections.IEnumerator Respawn(float duration)
    {    
        rigidbody2d.simulated = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        rigidbody2d.simulated = true;
        spriteRenderer.enabled = true;
        isDead = false;
    }
}
