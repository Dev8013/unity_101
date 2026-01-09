using UnityEngine;
using UnityEngine.UI;
using TMPro;   // add this

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth = 1000;

    [Header("UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;   // TMP instead of Text

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }

        UpdateHealthText();
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (healthSlider != null) healthSlider.value = currentHealth;
        UpdateHealthText();
        if (currentHealth <= 0) Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (healthSlider != null) healthSlider.value = currentHealth;
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = currentHealth + "/" + maxHealth;
    }
}
