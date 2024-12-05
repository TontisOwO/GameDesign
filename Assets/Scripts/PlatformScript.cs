using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] bool isStatic;
    [SerializeField] float waitTime = 1.0f;
    [SerializeField] Vector2 destination;
    Vector2 vectorValue0;
    [SerializeField] float timeTaken;
    Rigidbody2D myRidgidbody;
    private void Awake()
    {
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
