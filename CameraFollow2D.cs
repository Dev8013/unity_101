using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;          // player
    public float smoothSpeed = 5f;
    public float lookAheadX = 5f;     // how far ahead on X

    Vector3 baseOffset = new Vector3(0f, 0f, -10f);
    float fixedY;                     // locked vertical position

    void Start()
    {
        fixedY = transform.position.y;   // remember starting Y
    }

    void LateUpdate()
    {
        if (target == null) return;

        float horizontal = 0f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        else if (Input.GetKey(KeyCode.A)) horizontal = -1f;

        // Offset only on X based on direction
        float offsetX = 0f;
        if (horizontal > 0f) offsetX = lookAheadX;
        else if (horizontal < 0f) offsetX = -lookAheadX;

        Vector3 desiredPos = new Vector3(
            target.position.x + offsetX,
            fixedY,                      // keep Y constant
            baseOffset.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
    }
}
