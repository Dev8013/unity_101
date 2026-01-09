using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Transform player;          // drag player here

    [Header("UI")]
    public GameObject mapPanel;       // the whole panel to toggle
    public RectTransform playerArrow; // arrow image on the map

    [Header("Map Settings")]
    public float worldToMapScale = 0.1f; // tune this: how many pixels per world unit

    bool isOpen = false;

    void Start()
    {
        if (mapPanel != null)
            mapPanel.SetActive(false);
    }

    void Update()
    {
        // Toggle map with M
        if (Input.GetKeyDown(KeyCode.M))
        {
            isOpen = !isOpen;
            if (mapPanel != null)
                mapPanel.SetActive(isOpen);
        }

        // Update arrow position while map open
        if (isOpen && player != null && playerArrow != null)
        {
            Vector2 mapPos = new Vector2(
                player.position.x * worldToMapScale,
                player.position.y * worldToMapScale
            );

            playerArrow.anchoredPosition = mapPos;

            // Optional: rotate arrow to match player facing
            // playerArrow.rotation = Quaternion.Euler(0, 0, facingAngle);
        }
    }
}
