using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 25f;
    Animator animator;

    [SerializeField] Rigidbody2D rb;
    private bool isJumping;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        #region Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX != 0 && isJumping == false)
        {
            Running();
        }
        else if (moveX != 0 && isJumping == true)
        {
            Jumping();
        }
        else if(moveX == 0 && isJumping == false)
        {
            Idle();
        }
        

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                Jumping();
            }
        }
        
        #endregion

        #region FlipChar
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        #endregion
        
    }

    private void Idle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isZipzip", false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isRolling", false);
    }
    private void Running()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isZipzip", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRolling", false);
    }
    private void Jumping()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isZipzip", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRolling", false);
    }
    private void Rolling()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isZipzip", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRolling", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
