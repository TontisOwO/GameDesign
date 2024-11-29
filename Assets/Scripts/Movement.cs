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
            movementVector.x -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && jumpState == CharacterState.Grounded)
        {
            myRigidbody.linearVelocityY += jumpSpeed;
            jumpState = CharacterState.Jumping;
            Debug.Log("eh?");
        }

        if (jumpState == CharacterState.Jumping)
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
    public void Land()
    {
        jumpState = CharacterState.Grounded;
        jumpTime = 0;
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
