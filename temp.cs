// // // using UnityEngine;

// // // public class playerController : MonoBehaviour
// // // {
// // //     [Header("Movement")]
// // //     public float moveSpeed = 5f;
// // //     public float jumpForce = 10f;
// // //      public float boostForce = 3f;

// // //     [Header("Jump Settings")]
// // //     public int maxJumps = 2;
// // //     public float extraJumpForce = 12f;

// // //     [Header("Crouch")]
// // //     public float crouchScaleY = 0.5f;

// // //     [Header("Ground Check")]
// // //     public Transform groundCheck;
// // //     public float groundCheckRadius = 0.1f;
// // //     public LayerMask groundLayer;

// // //     [Header("Jump Cooldown")]
// // //     public float jumpCooldown = 0.5f;   // seconds between jumps
// // //     float lastJumpTime = -999f;

    

// // //     [Header("Dash")]
// // //     public float dashForce = 15f;
// // //     public float dashDuration = 0.15f;
// // //     public float dashCooldown = 1f;

// // //     bool isDashing = false;
// // //     float lastDashTime = -999f;

// // //     Rigidbody2D rb;
// // //     Vector3 originalScale;
// // //     int jumpsLeft;
// // //     bool isGrounded;
// // //     bool hasJumped;   // true after first jump, reset on ground
// // //     bool usedBoost;   // true after W boost, reset on ground
// // //     public int facingDir = 1;   // 1 = right, -1 = left


// // //     void Awake()
// // //     {
// // //         rb = GetComponent<Rigidbody2D>();
// // //         originalScale = transform.localScale;
// // //         jumpsLeft = maxJumps;
// // //     }

// // //     void Update()
// // // {
// // //     CheckGround();
// // //     HandleDashInput();
// // //     if (!isDashing)
// // //     {
// // //         HandleMove();
// // //         HandleJump();
// // //         HandleCrouch();
// // //     }
// // // }




    

 
// // // // }
//     void CheckGround()
//     {
//         isGrounded = Physics2D.OverlapCircle(
//             groundCheck.position,
//             groundCheckRadius,
//             groundLayer
//         );
//     }

// // // void HandleMove()
// // // {
// // //     float moveInput = 0f;

// // //     if (Input.GetKey(KeyCode.A)) moveInput = -1f;
// // //     else if (Input.GetKey(KeyCode.D)) moveInput = 1f;

// // //     rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

// // //     // update facing direction and flip sprite
// // //     if (moveInput != 0)
// // //     {
// // //         facingDir = moveInput > 0 ? 1 : -1;
// // //         Vector3 scale = transform.localScale;
// // //         scale.x = Mathf.Abs(scale.x) * facingDir;
// // //         transform.localScale = scale;
// // //     }
// // // }


// // //     void HandleJumpAndBoost()
// // //     {
// // //         // Single jump from ground with Space
// // //         if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped)
// // //         {
// // //             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
// // //             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
// // //             hasJumped = true;
// // //         }

// // //         // One‑time boost in air with W
// // //         if (Input.GetKeyDown(KeyCode.W) && !isGrounded && !usedBoost)
// // //         {
// // //             rb.AddForce(Vector2.up * boostForce, ForceMode2D.Impulse);
// // //             usedBoost = true;
// // //         }
// // //     }
    


// // // void HandleDashInput()
// // // {
// // //     if (Input.GetKeyDown(KeyCode.LeftShift) &&
// // //         !isDashing &&
// // //         Time.time >= lastDashTime + dashCooldown)
// // //     {
// // //         StartCoroutine(Dash());
// // //     }
// // // }

// // // System.Collections.IEnumerator Dash()
// // // {
// // //     isDashing = true;
// // //     lastDashTime = Time.time;

// // //     // Use facing direction (from your movement script)
// // //     int dir = facingDir != 0 ? facingDir : 1;
// // //     rb.linearVelocity = new Vector2(dir * dashForce, 0f);

// // //     // optional: disable gravity while dashing
// // //     float originalGravity = rb.gravityScale;
// // //     rb.gravityScale = 0f;

// // //     yield return new WaitForSeconds(dashDuration);

// // //     rb.gravityScale = originalGravity;
// // //     isDashing = false;
// // // }



// // //     void HandleCrouch()
// // //     {
// // //         if (Input.GetKey(KeyCode.S))
// // //             transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScaleY, originalScale.z);
// // //         else
// // //             transform.localScale = originalScale;
// // //     }

// // //     void OnDrawGizmosSelected()
// // //     {
// // //         if (groundCheck != null)
// // //         {
// // //             Gizmos.color = Color.green;
// // //             Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
// // //         }
// // //     }
// // // }

// // // player mele
// // using UnityEngine;

// // public class PlayerMelee : MonoBehaviour
// // {
// //     [Header("Sword & Attack")]
// //     public GameObject sword;          // child sword object
// //     public Transform attackPoint;     // child at sword tip
// //     public float attackRange = 1f;
// //     public LayerMask enemyLayer;      // layer for red box
// //     public int attackDamage = 100;
// //     public float attackCooldown = 0.4f;

// //     [Header("Idle hide sword")]
// //     public float idleHideTime = 5f;   // seconds without attacking

// //     float lastAttackTime = -999f;
// //     bool canAttack = true;

// //     public Vector2 swordOffsetRight = new Vector2(0.5f, 0f);
// //     public Vector2 swordOffsetLeft  = new Vector2(-0.5f, 0f);
// //     public Vector2 attackPointRight = new Vector2(1f, 0f);
// //     public Vector2 attackPointLeft  = new Vector2(-1f, 0f);

// //     playerController pc;

// //     void Awake()
// // {
// //     pc = GetComponent<playerController>();
// // }



// //     void Update()
// // {
// //     if (pc != null)
// //         UpdateSwordSide();

// //     if (Input.GetMouseButtonDown(0) && canAttack)
// //         Attack();

// //     if (Time.time - lastAttackTime > idleHideTime)
// //         SetSwordVisible(false);
// // }

// // void UpdateSwordSide()
// // {
// //     int dir = pc != null ? pc.facingDir : 1;

// //     if (sword != null)
// //     {
// //         Vector2 offset = dir == 1 ? swordOffsetRight : swordOffsetLeft;
// //         sword.transform.localPosition = offset;
// //         Vector3 sScale = sword.transform.localScale;
// //         sScale.x = Mathf.Abs(sScale.x) * dir;    // flip sword sprite
// //         sword.transform.localScale = sScale;
// //     }

// //     if (attackPoint != null)
// //     {
// //         Vector2 atkOffset = dir == 1 ? attackPointRight : attackPointLeft;
// //         attackPoint.localPosition = atkOffset;
// //     }
// // }

// //     void Attack()
// //     {
// //         lastAttackTime = Time.time;
// //         SetSwordVisible(true);
// //         canAttack = false;

// //         // Detect enemies in range
// //         Collider2D[] hits = Physics2D.OverlapCircleAll(
// //             attackPoint.position,
// //             attackRange,
// //             enemyLayer
// //         );

// //         foreach (Collider2D hit in hits)
// //         {
// //             Enemy e = hit.GetComponent<Enemy>();
// //             if (e != null)
// //             {
// //                 e.TakeDamage(attackDamage);
// //             }
// //         }

// //         // simple cooldown
// //         Invoke(nameof(ResetAttack), attackCooldown);
// //     }

// //     void ResetAttack()
// //     {
// //         canAttack = true;
// //     }

// //     void SetSwordVisible(bool visible)
// //     {
// //         if (sword != null)
// //         {
// //             sword.SetActive(visible);
// //         }
// //     }

// void HandleJump()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, 0f);
//             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//         }
//     }

// //     void OnDrawGizmosSelected()
//     {
//         if (groundCheck != null)
//         {
//             Gizmos.color = Color.green;
//             Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
//         }
//     }
// // }

