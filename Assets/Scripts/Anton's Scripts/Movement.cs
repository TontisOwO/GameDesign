using Unity.VisualScripting;
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

    [SerializeField] GameObject DustParticle;

    Animator myAnimator;
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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

            // Flip character to the left
            if (transform.localScale.x > 0)  // Check if the character is not already flipped
            {
                // Only flip the x scale, keep y and z the same
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
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

            // Flip character to the right
            if (transform.localScale.x < 0)  // Check if the character is flipped
            {
                // Only flip the x scale, keep y and z the same
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
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
            myAnimator.SetBool("pressJump", true);
        }

        if (jumpState == CharacterState.Jumping)
        {
            jumpTime += Time.deltaTime;
            myAnimator.SetBool("isMoving", false);

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            myAnimator.SetBool("pressJump", false);

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
            myAnimator.SetBool("isMoving", false);

        }

        if (Input.GetKeyDown(KeyCode.D) && jumpState == CharacterState.Grounded)
        {

            Instantiate(DustParticle, new Vector2(transform.position.x - 0.7f, transform.position.y - 8.4975f/2), Quaternion.Euler(0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.A) && jumpState == CharacterState.Grounded)
        {

            Instantiate(DustParticle, new Vector2(transform.position.x + 0.7f, transform.position.y - 8.4975f / 2), Quaternion.Euler(0, 180, 0));
        }
        if ((movingLeft ||movingRight) && jumpState == CharacterState.Grounded)
        {
            myAnimator.SetBool("isMoving", true);
        }
        if (!movingLeft && !movingRight)
        {
            myAnimator.SetBool("isMoving", false);
        }
        if (myRigidbody.linearVelocityY <= 0)
        {
            myAnimator.SetBool("pressJump", false);
        }
        transform.position = movementVector;
    }
    
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
    
    public void FallOff(Vector2 groundSpeed)
    {
        fellOff = true;
        myRigidbody.linearVelocity -= groundSpeed;
    }

}
