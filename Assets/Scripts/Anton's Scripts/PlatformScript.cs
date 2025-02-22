using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("Private variables")]

    [SerializeField] bool isStatic;
    [SerializeField] Vector2 destination;
    Vector2 vectorValue0;
    [SerializeField] float timeTaken;
    Rigidbody2D myRidgidbody;

    [Header("Public variables")]

    public Vector2 moveVector;
    private void Awake()
    {
        moveVector = destination;
        vectorValue0 = Vector2.zero;
        myRidgidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        timeTaken += Time.deltaTime;
        if (!isStatic)
        {
            switch (timeTaken)
            {
                case <= 1f:
                    myRidgidbody.linearVelocity = Vector2.Lerp(vectorValue0, destination, 1);
                    break;
                case <= 2f:
                    myRidgidbody.linearVelocity = vectorValue0;
                    break;
                case <= 3f:
                    myRidgidbody.linearVelocity = Vector2.Lerp(vectorValue0, -destination, 1);
                    break;
                case <= 4f:
                    myRidgidbody.linearVelocity = vectorValue0;
                    break;
                case <= 5f:
                    timeTaken = 0;
                    break;
            }
        }
    }
}
