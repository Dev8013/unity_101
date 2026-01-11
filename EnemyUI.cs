using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public static EnemyUI Instance;

    [Header("Target")]
    public Enemy enemy;                // assign current enemy box
    public Transform player;
    public float showDistance = 8f;

    [Header("UI")]
    public Slider bossHealthSlider;    // red bar
    public Slider bossShieldSlider;    // yellow bar
    // public TextMeshProUGUI bossLevelText;

    void Awake()
    {
        Instance = this;
    }

// void Start()
// {
//     if (enemy == null) return;

//     bossHealthSlider.maxValue = enemy.maxHealth;
//     bossHealthSlider.value    = enemy.currentHealth;

//     bossShieldSlider.maxValue = enemy.maxShield;
//     bossShieldSlider.value    = enemy.currentShield;
// }
void Start()
{
    if (enemy == null) return;

    bossHealthSlider.maxValue = enemy.maxHealth;
    bossHealthSlider.value    = enemy.currentHealth;

    bossShieldSlider.maxValue = enemy.maxShield;
    bossShieldSlider.value    = enemy.currentShield;
}



    void Update()
    {
        if (enemy == null || player == null) return;

        float dist = Vector2.Distance(player.position, enemy.transform.position);
        bool show = dist <= showDistance;

        bossHealthSlider.gameObject.SetActive(show);
        bossShieldSlider.gameObject.SetActive(show);
        // bossLevelText.gameObject.SetActive(show);
    }

public void UpdateBars(Enemy e)
{
    if (bossHealthSlider != null)
        bossHealthSlider.value = e.currentHealth;

    if (bossShieldSlider != null)
        bossShieldSlider.value = e.currentShield;
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
    
}

}
