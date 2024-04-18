using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9;
    [SerializeField] private float jumpHeight = 650;
    [SerializeField] private float jumpForce = 8f;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject SecretTiles;
    [SerializeField] private GameObject HiddenDamagingTiles;

    [SerializeField] private GameObject JumpingPad;

    [SerializeField] ParticleSystem dyingParticle;
    [SerializeField] private GameObject bloodDropsFX;

    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float dmgSmoothTime = .5f;

    private float currentHealth;
    private Animator animator;
    private Rigidbody2D player;
    private bool isFacingRight;
    public bool isGrounded;
    private bool isAlive;

    private Vector2 startPos;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private GameObject projectile;

    [SerializeField] private float fireRate = 0.2f;
    private float nextFire;

    private bool hasWeapon = false;

    [SerializeField] private AudioClip grunt;

    private AudioSource audioS;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();

        dyingParticle.Pause();

        isFacingRight = true;
        isGrounded = false; 
        isAlive = true;

        SecretTiles.SetActive(true);
        HiddenDamagingTiles.SetActive(false);
        JumpingPad.SetActive(false);

        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        startPos = transform.position;
    }


    private void Update()
    {
        healthBar.value = currentHealth;
        if (isAlive && player.position.y < -30)
        {
            Die();
            isAlive = false;
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

        if (CoinCounter.Instance.score == 6)
        {
            JumpingPad.SetActive(true);
        }

        //if (isOnJumpPad && isFacingRight && !isGrounded)
        //{
        //    player.AddForce(new Vector2(0, jumpHeight * 0.5f));
        //    isOnJumpPad = false;
        //}

        if (Time.time >= nextFire && Input.GetAxisRaw("Fire1") != 0 && hasWeapon)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, gunMuzzle.position, Quaternion.Euler(0, 0,
                z: isFacingRight ? 0 : 180));
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

    public void TakeDamage(float damage)
    {
        audioS.PlayOneShot(grunt);
        currentHealth -= damage;
        healthBar.value = currentHealth;
        Instantiate(bloodDropsFX, transform.position, transform.rotation);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GetHP(float hp)
    {
        currentHealth = hp;
    }

    private void Die()
    {
        audioS.pitch = .5f;
        audioS.PlayOneShot(grunt);

        isAlive = false;
        hasWeapon = false;
        dyingParticle.Play();
        StartCoroutine(Respawn(.6f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.CompareTag("Secret1"))
        {
            SecretTiles.SetActive(false);
        }

        if (collision.CompareTag("JumpPad") && !isGrounded)
        {
            player.AddForce(new Vector2(0, jumpHeight * 2f));
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Weapon"))
        {
            Transform weaponTransform = collision.transform;
            weaponTransform.parent = transform;

            if (isFacingRight)
            {
                weaponTransform.localPosition = new Vector3(0.3f, -0.1f, 0f);
            }
            else
            {
                weaponTransform.localPosition = new Vector3(0.3f, -0.1f, 0f);
                weaponTransform.localScale = new Vector3(
             x: weaponTransform.localScale.x * -1,
             y: weaponTransform.localScale.y,
             z: weaponTransform.localScale.z);
            }

            weaponTransform.GetComponent<Collider2D>().enabled = false;

            hasWeapon = true;

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
        isAlive = true;
        SecretTiles.SetActive(true);
        HiddenDamagingTiles.SetActive(false);
        JumpingPad.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }
}