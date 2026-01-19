using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy for Map 1 (lv 1 to 20)
    public int level = 100;

    [Header("Health")]
    private int maxHealth = 1000;
    private int currentHealth = 1000;


    [Header("Shield")]
    private float shieldPercent = 0.3f;   // 30%
    private int maxShield;
    private int currentShield;

  void Start()
    {
        currentHealth = maxHealth;

        maxShield = (int)(maxHealth * shieldPercent);
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
