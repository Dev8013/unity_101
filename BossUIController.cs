using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUIController : MonoBehaviour
{
    public Transform player;
    public Enemy enemy;              // assign your red box here
    public float showDistance = 5f;  // how close player must be

    [Header("UI")]
    public Slider bossHealthSlider;
    public TextMeshProUGUI bossLevelText;

    void Start()
    {
        bossHealthSlider.gameObject.SetActive(false);
        bossLevelText.gameObject.SetActive(false);

        bossHealthSlider.maxValue = enemy.maxHealth;
        bossHealthSlider.value = enemy.currentHealth;
        bossLevelText.text = enemy.level + "Lvl";
    }

    void Update()
    {
        if (enemy == null) return;

        float dist = Vector2.Distance(player.position, enemy.transform.position);

        bool show = dist <= showDistance;
        bossHealthSlider.gameObject.SetActive(show);
        bossLevelText.gameObject.SetActive(show);

        if (show)
        {
            bossHealthSlider.value = enemy.currentHealth;
        }
    }
}
