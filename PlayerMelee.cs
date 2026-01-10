using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header("Sword & Attack")]
    public GameObject sword;          // child sword object
    public Transform attackPoint;     // child at sword tip
    public float attackRange = 1f;
    public LayerMask enemyLayer;      // layer for red box
    public int attackDamage = 100;
    public float attackCooldown = 0.4f;

    [Header("Idle hide sword")]
    public float idleHideTime = 5f;   // seconds without attacking

    float lastAttackTime = -999f;
    bool canAttack = true;

    public Vector2 swordOffsetRight = new Vector2(0.5f, 0f);
    public Vector2 swordOffsetLeft  = new Vector2(-0.5f, 0f);
    public Vector2 attackPointRight = new Vector2(1f, 0f);
    public Vector2 attackPointLeft  = new Vector2(-1f, 0f);

    playerController pc;

    void Awake()
{
    pc = GetComponent<playerController>();
}



    void Update()
{
    if (pc != null)
        UpdateSwordSide();

    if (Input.GetMouseButtonDown(0) && canAttack)
        Attack();

    if (Time.time - lastAttackTime > idleHideTime)
        SetSwordVisible(false);
}

void UpdateSwordSide()
{
    int dir = pc != null ? pc.facingDir : 1;

    if (sword != null)
    {
        Vector2 offset = dir == 1 ? swordOffsetRight : swordOffsetLeft;
        sword.transform.localPosition = offset;
        Vector3 sScale = sword.transform.localScale;
        sScale.x = Mathf.Abs(sScale.x) * dir;    // flip sword sprite
        sword.transform.localScale = sScale;
    }

    if (attackPoint != null)
    {
        Vector2 atkOffset = dir == 1 ? attackPointRight : attackPointLeft;
        attackPoint.localPosition = atkOffset;
    }
}

    void Attack()
    {
        lastAttackTime = Time.time;
        SetSwordVisible(true);
        canAttack = false;

        // Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            Enemy e = hit.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(attackDamage);
            }
        }

        // simple cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void SetSwordVisible(bool visible)
    {
        if (sword != null)
        {
            sword.SetActive(visible);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
