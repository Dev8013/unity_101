using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;          // player
    public float smoothSpeed = 5f;
    public float lookAheadX = 5f;     // how far ahead on X
    public Vector3 baseOffset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        // Input-based look-ahead on X
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        else if (Input.GetKey(KeyCode.A)) horizontal = -1f;

        float offsetX = 0f;
        if (horizontal > 0f) offsetX = lookAheadX;
        else if (horizontal < 0f) offsetX = -lookAheadX;

        // Follow player on both X and Y
        Vector3 desiredPos = new Vector3(
            target.position.x + offsetX,
            target.position.y,          // <‑ follow Y too
            baseOffset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
