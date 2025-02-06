using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float CameraMovementSpeed;
    [SerializeField] Vector2 MinBounds;
    [SerializeField] Vector2 MaxBounds;
    float zOffset;

    void Start()
    {
        zOffset = transform.position.z - PlayerTransform.position.z;
    }

    void Update()
    {
        // Framerate consistent movement
        float frameRateConsistent = Mathf.Pow(0.5f, Time.deltaTime * CameraMovementSpeed);
        Vector3 targetPosition = Vector3.Lerp(PlayerTransform.position + new Vector3(0, 0, zOffset), transform.position, frameRateConsistent);

        // Apply boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, MinBounds.x, MaxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, MinBounds.y, MaxBounds.y);

        transform.position = targetPosition;
    }
}
