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
    [SerializeField] float AccelDeccelSpeed;
    [SerializeField] float smallHopFall = 0.5f;
    [SerializeField] float coyoteTime;
    float movementSpeedLeft;
    float movementSpeedRight;
    bool movingLeft;
    bool movingRight;
    bool smallHop;
    [SerializeField] float jumpSpeed;
    float jumpFactor;
    float jumpTime;
    [SerializeField] CharacterState jumpState;
    Vector2 movementVector;
    [SerializeField] float timeAfterWalkOff;
    bool fellOff = true;
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movementVector = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            movingLeft = true;
        }
        if (!Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            movingLeft = false;
        }
        if (movingLeft)
        {
            if (movementSpeedLeft < movementSpeed)
            {
                movementSpeedLeft += Time.deltaTime * movementSpeed * AccelDeccelSpeed;
            }
            movementVector.x -= movementSpeedLeft * Time.deltaTime;
        }
        else if (movementSpeedLeft > movementSpeed * 0.25f)
        {
            movementSpeedLeft -= Time.deltaTime * movementSpeed * AccelDeccelSpeed;
            movementVector.x -= movementSpeedLeft * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movingRight = true;
        }
        if (!Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            movingRight = false;

        }
        if (movingRight)
        {
            if (movementSpeedRight < movementSpeed)
            {
                movementSpeedRight += Time.deltaTime * movementSpeed * AccelDeccelSpeed;
            }
            movementVector.x += movementSpeedRight * Time.deltaTime;
        }
        else if (movementSpeedRight > movementSpeed * 0.25f)
        {
            movementSpeedRight -= Time.deltaTime * movementSpeed * AccelDeccelSpeed;
            movementVector.x += movementSpeedRight * Time.deltaTime;
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
                    smallHop = true;
                    break;

                case ( > 0.1f, <= 0.25f):
                    jumpFactor = 0.25f;
                    break;

                case ( > 0.25f, <= 100f):
                    jumpFactor = 0f;
                    break;
            }
            myRigidbody.linearVelocityY -= jumpFactor * jumpSpeed;
            jumpTime = 0;
        }
        if (smallHop)
        {
            myRigidbody.linearVelocityY -= smallHopFall * jumpSpeed * Time.deltaTime;
            if (myRigidbody.linearVelocityY < 0) 
            {
                smallHop = false;
            }
        }
        if (Input.GetKey(KeyCode.S) && jumpState == CharacterState.Air)
        {
            movementVector.y -= movementSpeed * Time.deltaTime;
        }
        if (fellOff)
        {
            timeAfterWalkOff += Time.deltaTime;
        }
        if (timeAfterWalkOff > coyoteTime) 
        {
            timeAfterWalkOff = 0;
            jumpState = CharacterState.Air;
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
        fellOff = false;
    }
    //void OnCollisionExit2D(Collision2D other)
    //{
    //    if (jumpState != CharacterState.Jumping)
    //    {
    //        FallOff();
    //    }
    //}
    public void FallOff(Vector2 groundSpeed)
    {
        fellOff = true;
        myRigidbody.linearVelocity -= groundSpeed;
    }
}
