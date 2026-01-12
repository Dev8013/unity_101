using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public static EnemyUI Instance;

    [Header("Target")]
    public Enemy enemy;
    public Transform player;
    public float showDistance = 8f;

    [Header("UI")]
    public Slider bossHealthSlider;
    public Slider bossShieldSlider;
    public TextMeshProUGUI enemyHpText;   // << NEW

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (enemy == null) return;

        bossHealthSlider.maxValue = enemy.maxHealth;
        bossHealthSlider.value    = enemy.currentHealth;

        bossShieldSlider.maxValue = enemy.maxShield;
        bossShieldSlider.value    = enemy.currentShield;

        UpdateBars(enemy); // set initial text
    }

    void Update()
    {
        if (enemy == null || player == null) return;

        float dist = Vector2.Distance(player.position, enemy.transform.position);
        bool show = dist <= showDistance;

        if (bossHealthSlider != null)
            bossHealthSlider.gameObject.SetActive(show);
        if (bossShieldSlider != null)
            bossShieldSlider.gameObject.SetActive(show);
        if (enemyHpText != null)
            enemyHpText.gameObject.SetActive(show);
    }

    public void UpdateBars(Enemy e)
    {
        if (bossHealthSlider != null)
            bossHealthSlider.value = e.currentHealth;

        if (bossShieldSlider != null)
            bossShieldSlider.value = e.currentShield;

        if (enemyHpText != null)
            enemyHpText.text = e.currentHealth + "/" + e.maxHealth;
    }

    public void OnShieldBroken()
    {
        if (bossShieldSlider != null)
        {
            Destroy(bossShieldSlider.gameObject);
            bossShieldSlider = null;
        }
    }

    public void OnHealthEmpty()
    {
        if (bossHealthSlider != null)
        {
            Destroy(bossHealthSlider.gameObject);
            bossHealthSlider = null;
        }
        if (enemyHpText != null)
        {
            Destroy(enemyHpText.gameObject);
            enemyHpText = null;
        }
    }
}
