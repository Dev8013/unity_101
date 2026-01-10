using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 100;
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        // TODO: update boss UI health bar here if you have a reference

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // you can play animation / sound here later
        Destroy(gameObject);
    }
}
