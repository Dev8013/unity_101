using UnityEngine;

public class playerController : MonoBehaviour
{
    // [Header("Movement")]
    // public float moveSpeed = 5f;
    // public float jumpForce = 10f;
    // public float boostForce = 3f;
    [Header("Movement")]
public float moveSpeed = 5f;
public float jumpForce = 10f;
public float extraJumpForce = 10f;   // second‑jump force


    // [Header("Jump Settings")]
    // public int maxJumps = 2;
    // public float extraJumpForce = 12f;
    [Header("Jump Settings")]
public int maxJumps = 2;      // total jumps allowed (ground + air)
int jumpsLeft;



    [Header("Crouch")]
    public float crouchScaleY = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // [Header("Jump Cooldown")]
    // public float jumpCooldown = 0.5f;
    // float lastJumpTime = -999f;

    [Header("Dash")]
    public float dashForce = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    bool isDashing = false;
    float lastDashTime = -999f;

    Rigidbody2D rb;
    Vector3 originalScale;
    bool isGrounded;
    bool hasJumped;
    bool usedBoost;
    public int facingDir = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        jumpsLeft = maxJumps; //refactor jump system

    }

    void Update()
    {
        CheckGround();
        HandleDashInput();

        if (!isDashing)
        {
            HandleMove();
            HandleJumpAndBoost();
            HandleCrouch();
        }
    }

    // void CheckGround()
    // {
    //     isGrounded = Physics2D.OverlapCircle(
    //         groundCheck.position,
    //         groundCheckRadius,
    //         groundLayer
    //     );

    //     if (isGrounded)
    //     {
    //         hasJumped = false;
    //         usedBoost = false;
    //     }
    // }
    void CheckGround()
{
    isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundCheckRadius,
        groundLayer
    );

    if (isGrounded)
    {
        jumpsLeft = maxJumps;   // reset to 2 when on ground
    }
}


    void HandleMove()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        else if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            facingDir = moveInput > 0 ? 1 : -1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * facingDir;
            transform.localScale = scale;
        }
    }

    // single jump + W boost (no double jump)
    // void HandleJumpAndBoost()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped &&
    //         Time.time >= lastJumpTime + jumpCooldown)
    //     {
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    //         rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //         hasJumped = true;
    //         lastJumpTime = Time.time;
    //     }

    //     if (Input.GetKeyDown(KeyCode.W) && !isGrounded && !usedBoost)
    //     {
    //         rb.AddForce(Vector2.up * boostForce, ForceMode2D.Impulse);
    //         usedBoost = true;
    //     }
    // }
    void HandleJumpAndBoost()
{
    // Simple double jump: Space up to maxJumps times
    if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
    {
        // optional: stronger first jump, weaker second
        float force = (jumpsLeft == maxJumps) ? jumpForce : extraJumpForce;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        jumpsLeft--;
    }
}



    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            !isDashing &&
            Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        int dir = facingDir != 0 ? facingDir : 1;
        rb.linearVelocity = new Vector2(dir * dashForce, 0f);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
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
