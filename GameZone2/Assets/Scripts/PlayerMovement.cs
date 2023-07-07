using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    Animator animator;

    [SerializeField] Rigidbody2D rb;
    private bool isJumping;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if(moveX != 0)
        {  
            animator.SetBool("isRunning", true);
 
        }
        else
        {
            animator.SetBool("isRunning", false);

        }
        

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                
            }
        }
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
