using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9;
    [SerializeField] private float jumpHeight = 650;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject SecretTiles;
    [SerializeField] private GameObject HiddenDamagingTiles;

    [SerializeField] private GameObject JumpingPad;
    

    [SerializeField] private GameObject Coins;

    [SerializeField] ParticleSystem dyingParticle;

    private Animator animator;
    private Rigidbody2D player;
    private bool isFacingRight;
    private bool isGrounded;
    private bool isDead;
    private bool isOnJumpPad;

    private Vector2 startPos;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();

        dyingParticle.Pause();

        isFacingRight = true;
        isGrounded = false;
        isOnJumpPad = false;
        isDead = false;

        SecretTiles.SetActive(true);
        HiddenDamagingTiles.SetActive(false);
        JumpingPad.SetActive(false);
        Coins.SetActive(true);

        startPos = transform.position;
    }
    

    private void Update()
    {
        if (!isDead && player.position.y < -30)
        {
            Die();
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            player.AddForce(new Vector2(0, jumpHeight));
        }

        if ((player.position.x < -19.6 &&
            player.position.y < -8) && (player.position.x > -24.5 && player.position.y > -9))
        {
            HiddenDamagingTiles.SetActive(true);
        }

        if (player.position.x > -19.8 && player.position.x < -18 && player.position.y < -8 && player.position.y > -8.5 && Input.GetButtonDown("Jump") && isFacingRight)
        {
            if (!isGrounded)
            {
                JumpingPad.SetActive(true);
                isOnJumpPad = true;
            }
        }

        if (isOnJumpPad && isFacingRight && !isGrounded)
        {
            player.AddForce(new Vector2(0, jumpHeight * 0.5f));
            isOnJumpPad = false;
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));
        player.velocity = new Vector2(move * maxSpeed, player.velocity.y);
        if ((move < 0 && isFacingRight) || (move > 0 && !isFacingRight)) Flip();

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, .15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", player.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(
            x: transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }

    private void Die()
    {
        isDead = true;
        dyingParticle.Play();
        StartCoroutine(Respawn(.6f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamagingObstacle"))
        {
            Die();
        }

        if (collision.CompareTag("Secret1"))
        {
            SecretTiles.SetActive(false);
        }
        if (collision.CompareTag("JumpPad"))
        {
            player.AddForce(new Vector2(0, jumpHeight * 2));
        }
        if (collision.CompareTag("Coin"))
        {
            CoinCounter.currentCoins += 1;
            Coins.SetActive(false);
        }
    }

    IEnumerator Respawn(float duration)
    {
        player.simulated = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        player.simulated = true;
        spriteRenderer.enabled = true;
        isDead = false;
        SecretTiles.SetActive(true);
        HiddenDamagingTiles.SetActive(false);
        JumpingPad.SetActive(false);
        Coins.SetActive(true);
        CoinCounter.currentCoins = 0;
    }
}
