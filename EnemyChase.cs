using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    public Transform player;          // drag player here
    public float moveSpeed = 3f;

    [Header("Ranges")]
    public float detectRange = 8f;    // start/stop chasing
    public float attackRange = 1.2f;  // distance to hit

    [Header("Attack")]
    public int damage = 100;
    public float attackCooldown = 1f;

    Rigidbody2D rb;
    float lastAttackTime = -999f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // If outside detection range, stop moving.
        if (dist > detectRange)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Within detection range → chase
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);

        // If close enough, attack
        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        playerHealth ph = player.GetComponent<playerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
    }
}
