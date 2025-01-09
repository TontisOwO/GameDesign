using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float CameraMovementSpeed;
    float zOffset;

    void Start()
    {
        zOffset = transform.position.z - PlayerTransform.position.z;
    }

    void Update()
    {
        //I, Anton, have edited a bit here so that it's framerate consistent because it wasn't
        float frameRateConsistent = Mathf.Pow(0.5f, Time.deltaTime * CameraMovementSpeed);
        transform.position =  Vector3.Lerp(PlayerTransform.position + new Vector3(0, 0, zOffset), transform.position, frameRateConsistent);
    }
}
