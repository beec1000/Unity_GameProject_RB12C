using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9;
    [SerializeField] private float jumpHeight = 650;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;


    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private bool isFacingRight;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        isGrounded = false;
    }
    private void Update()
    {
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
            x:transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }
}
