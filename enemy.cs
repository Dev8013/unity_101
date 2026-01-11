using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 100;

    [Header("Health")]
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    [Header("Shield")]
    public float shieldPercent = 0.3f;   // 30%
    public int maxShield;
    public int currentShield;

  void Start()
    {
        currentHealth = maxHealth;

        maxShield = Mathf.RoundToInt(maxHealth * shieldPercent);
        currentShield = maxShield;

        // Debug to verify
        Debug.Log($"Enemy shield init: maxShield={maxShield}, currentShield={currentShield}");
    }


    public void TakeDamage(int amount)
{
    // damage shield first
    if (currentShield > 0)
    {
        int shieldDamage = Mathf.Min(amount, currentShield);
        currentShield -= shieldDamage;
        amount -= shieldDamage;

        if (currentShield <= 0)
        {
            EnemyUI.Instance?.OnShieldBroken();
        }
    }

    // remaining damage goes to HP
    if (amount > 0)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            EnemyUI.Instance?.OnHealthEmpty();
            Die();
            return;
        }
    }

    EnemyUI.Instance?.UpdateBars(this);
}

    void Die()
    {
        Destroy(gameObject);
    }
}
