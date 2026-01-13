using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    bool isLocked = false;

    void Start()
    {
        // Optional: start locked
        SetCursorLocked(true);
    }

    void Update()
    {
        // Toggle on Left Control press
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetCursorLocked(!isLocked);
        }
    }

    void SetCursorLocked(bool locked)
    {
        isLocked = locked;

        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;   // lock to center
            Cursor.visible = false;                     // hide cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;     // free cursor
            Cursor.visible = true;                      // show cursor
        }
    }
}
