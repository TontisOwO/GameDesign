using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int LifeTotal = 3;
    public int EnemySpeed = 5;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.right * EnemySpeed;
    }
}
