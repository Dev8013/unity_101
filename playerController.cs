using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
     public float boostForce = 3f;

    [Header("Jump Settings")]
    public int maxJumps = 2;
    public float extraJumpForce = 12f;

    [Header("Crouch")]
    public float crouchScaleY = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Vector3 originalScale;
    int jumpsLeft;
    bool isGrounded;
    bool hasJumped;   // true after first jump, reset on ground
    bool usedBoost;   // true after W boost, reset on ground
    public int facingDir = 1;   // 1 = right, -1 = left


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        CheckGround();
        HandleMove();
        HandleJump();
        HandleCrouch();
        HandleJumpAndBoost();
    }




    

//     void HandleJump()
// {
//     // Space pressed this frame?
//     if (Input.GetKeyDown(KeyCode.Space))
//     {
//         // 1) First jump: only when grounded
//         if (isGrounded && jumpsLeft == maxJumps)
//         {
//             float force = Input.GetKey(KeyCode.W) ? extraJumpForce : jumpForce;

//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
//             rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

//             jumpsLeft--;          // now jumpsLeft = 1
//             return;
//         }

//         // 2) Double jump: in air, still has a jump left
//         if (!isGrounded && jumpsLeft > 0)
//         {
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

//             jumpsLeft = 0;
//         }
//     }
// }
void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            hasJumped = false;  // can jump again
            usedBoost = false;  // can boost again
        }
    }

void HandleMove()
{
    float moveInput = 0f;

    if (Input.GetKey(KeyCode.A)) moveInput = -1f;
    else if (Input.GetKey(KeyCode.D)) moveInput = 1f;

    rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

    // update facing direction and flip sprite
    if (moveInput != 0)
    {
        facingDir = moveInput > 0 ? 1 : -1;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }
}


    void HandleJumpAndBoost()
    {
        // Single jump from ground with Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasJumped = true;
        }

        // One‑time boost in air with W
        if (Input.GetKeyDown(KeyCode.W) && !isGrounded && !usedBoost)
        {
            rb.AddForce(Vector2.up * boostForce, ForceMode2D.Impulse);
            usedBoost = true;
        }
    }
    
void HandleJump()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.S))
            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScaleY, originalScale.z);
        else
            transform.localScale = originalScale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
