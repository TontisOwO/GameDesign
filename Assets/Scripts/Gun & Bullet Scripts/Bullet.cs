using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int baseDamage = 25;
    private int damage;
    private Rigidbody2D rb;

    public bool isCharging = false;

    void Start()
    {
        if (!isCharging)
        {
            rb = GetComponent<Rigidbody2D>();

            GameObject barrel = GameObject.Find("barrel");
            if (barrel != null)
            {
                rb.linearVelocity = transform.right * barrel.transform.localScale.x * bulletSpeed;
            }
        }
    }

    // Method to set the charge multiplier
    public void SetCharge(float chargeMultiplier)
    {
        // Update damage and size based on the charge multiplier
        damage = Mathf.RoundToInt(baseDamage * chargeMultiplier);
        transform.localScale *= chargeMultiplier; // Scale the bullet size
        Debug.Log($"Bullet created with damage: {damage} and scale: {transform.localScale}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCharging)
            return; // Prevent charging bullets from interacting

        // Check if the object hit has the EnemyHealth script
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"Bullet hit {collision.gameObject.name}, dealing {damage} damage.");
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
