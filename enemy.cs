using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 100;
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
}
