using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the barrel GameObject
        GameObject barrel = GameObject.Find("barrel");
        if (barrel != null)
        {
            // Set bullet velocity based on the barrel's localScale direction
            rb.linearVelocity = transform.right * barrel.transform.localScale.x * bulletSpeed;
        }
        else
        {
            Debug.LogError("Barrel GameObject not found!");
        }
    }
}
