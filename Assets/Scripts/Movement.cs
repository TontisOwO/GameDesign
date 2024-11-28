using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpSpeed;
    float jumpFactor;
    float jumpTime;
    [SerializeField] bool jumping;
    [SerializeField] bool air;
    Vector2 movementVector;
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movementVector = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && !air && !jumping)
        {
            myRigidbody.linearVelocity += new Vector2(0, jumpSpeed);
            jumping = true;
            Debug.Log("eh?");
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (jumpTime, jumpTime)
            {
                case ( > 0, <= 0.1f):
                    jumpFactor = 0.5f;
                    break;

                case ( > 0.1f, <= 0.25f):
                    jumpFactor = 0.25f;
                    break;

                case ( > 0.25f, <= 100f):
                    jumpFactor = 0f;
                    break;
            }
            myRigidbody.linearVelocity -= new Vector2(0, jumpFactor * jumpSpeed);
            jumpTime = 0;
            jumping = false;
            air = true;
        }
        transform.position = movementVector;
    }
    public void Land()
    {
        Debug.Log("ha gayer");
        air = false;
        jumping = false;
        jumpTime = 0;
    }
    public void FallOf()
    {
        if (!jumping)
        {
            air = true;
        }
        Debug.Log("lamoer");
    }
}
