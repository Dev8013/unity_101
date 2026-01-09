using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 100;   // how much HP to remove

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth health = collision.gameObject.GetComponent<playerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
