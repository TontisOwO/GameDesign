using UnityEngine;

public class GroudCheck : MonoBehaviour
{
    [SerializeField] Movement parent;
    private void Awake()
    {
        parent = GetComponentInParent<Movement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (other.GetComponent<Rigidbody2D>().linearVelocityY > 0)
            {
                parent.Land(other.GetComponent<Rigidbody2D>().linearVelocityY);
            }
            else
            {
                parent.Land(other.GetComponent<Rigidbody2D>().linearVelocityY);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (other.GetComponent<Rigidbody2D>().linearVelocityY > 0)
            {
                parent.OnGround(other.GetComponent<Rigidbody2D>().linearVelocityY);
            }
            else
            {
                parent.OnGround(other.GetComponent<Rigidbody2D>().linearVelocityY);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlatformScript>() != null)
        {
            parent.FallOff(other.GetComponent<PlatformScript>().moveVector);
        }
        else
        {
            parent.FallOff(new Vector2(0, 0));
        }
    }
}
