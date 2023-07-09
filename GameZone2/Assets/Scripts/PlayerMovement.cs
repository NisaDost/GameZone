using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 25f;
    Animator animator;

    [SerializeField] Rigidbody2D rb;

    private GroundCheck groundCheck;
    bool ZipliyoMu;
    private bool IsRolling = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        groundCheck= GetComponent<GroundCheck>();
        ZipliyoMu = groundCheck.isJumping;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        #region Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX != 0 && ZipliyoMu == false)
        {
            Running();
        }
        else if (moveX != 0 && ZipliyoMu == true)
        {
            Jumping();
        }
        else if(moveX == 0 && ZipliyoMu == false)
        {
            Idle();
        }
        

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
        {
            if (!ZipliyoMu)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                ZipliyoMu = true;
                Jumping();
            }
        }


        if(Input.GetKeyDown(KeyCode.LeftShift) && !IsRolling)
        {
            
            //StartRollAnimation();
            
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
    //TRIGGER KULLAN!!!
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
        
    }
    private void Jumping()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isZipzip", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRolling", false);
    }
    //private void Rolling()
    //{
    //    animator.SetBool("isRunning", false);
    //    animator.SetBool("isZipzip", false);
    //    animator.SetBool("isIdle", false);
    //    animator.SetBool("isRolling", true);
    //}

    //void StartRollAnimation()
    //{
    //    animator.SetBool("isRolling", true);
    //    IsRolling = true;
    //    Invoke("EndRollAnimation", -1.5f);
    //}

    //void EndRollAnimation()
    //{
    //    animator.SetBool("isRolling", false);
    //    IsRolling = false;
    //}
    
}
