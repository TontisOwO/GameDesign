using UnityEngine;

enum CharacterState
{
    Grounded,
    Jumping,
    Air
}
public class Movement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float movementSpeed;
    float movementSpeedLeft;
    float movementSpeedRight;
    [SerializeField] float jumpSpeed;
    float jumpFactor;
    float jumpTime;
    [SerializeField] CharacterState jumpState;
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
            if (movementSpeedLeft < movementSpeed)
            {
                movementSpeedLeft += Time.deltaTime * movementSpeed * 5;
            }
            movementVector.x -= movementSpeedLeft * Time.deltaTime;
        }
        if (!Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            movementSpeedLeft = movementSpeed * 0.25f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (movementSpeedRight < movementSpeed)
            {
                movementSpeedRight += Time.deltaTime * movementSpeed * 5;
            }
            movementVector.x += movementSpeedRight * Time.deltaTime;
        }
        if (!Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            movementSpeedRight = movementSpeed * 0.25f;

        }

        if (Input.GetKey(KeyCode.Space) && jumpState == CharacterState.Grounded)
        {
            if (myRigidbody.linearVelocityY < 0)
            {
                myRigidbody.linearVelocityY = 0;
            }
            myRigidbody.linearVelocityY += jumpSpeed;
            jumpState = CharacterState.Jumping;
        }

        if (jumpState == CharacterState.Jumping)
        {
            jumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpState = CharacterState.Air;

            switch (jumpTime, jumpTime)
            {
                case ( > 0, <= 0.1f):
                    jumpFactor = 0.7f;
                    break;

                case ( > 0.1f, <= 0.25f):
                    jumpFactor = 0.3f;
                    break;

                case ( > 0.25f, <= 100f):
                    jumpFactor = 0f;
                    break;
            }
            myRigidbody.linearVelocity -= new Vector2(0, jumpFactor * jumpSpeed);
            jumpTime = 0;
        }

        transform.position = movementVector;
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Floor"))
    //    {
    //        Land();
    //    }
    //}
    public void Land(float groundSpeed)
    {
        jumpState = CharacterState.Grounded;
        myRigidbody.linearVelocityY = groundSpeed;
        jumpTime = 0;
    }
    public void OnGround(float groundSpeed)
    {
        myRigidbody.linearVelocityY = groundSpeed;
        jumpState = CharacterState.Grounded;
    }
    //void OnCollisionExit2D(Collision2D other)
    //{
    //    if (jumpState != CharacterState.Jumping)
    //    {
    //        FallOff();
    //    }
    //}
    public void FallOff()
    {
        if (jumpState != CharacterState.Jumping)
        {
            jumpState = CharacterState.Air;
        }
    }
}
