using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damage = 25;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject barrel = GameObject.Find("barrel");
        if (barrel != null)
        {
            rb.linearVelocity = transform.right * barrel.transform.localScale.x * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
