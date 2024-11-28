using UnityEngine;

public class Camera : MonoBehaviour
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
        transform.position =  Vector3.Lerp(transform.position, PlayerTransform.position + new Vector3 (0,0,zOffset),Time.deltaTime * CameraMovementSpeed);
    }
}
