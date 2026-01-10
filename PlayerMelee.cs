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

    void Update()
    {
        // Left mouse click = attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        // Hide sword if idle too long
        if (Time.time - lastAttackTime > idleHideTime)
        {
            SetSwordVisible(false);
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
