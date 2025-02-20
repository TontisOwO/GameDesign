using UnityEngine;

public class PickupScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null && 
            collision.gameObject.GetComponent<PlayerHealth>().health < collision.gameObject.GetComponent<PlayerHealth>().maxHealth)
        {
            collision.gameObject.GetComponent<PlayerHealth>().health += 1;
            Destroy(gameObject);
            Debug.Log("healed!");
        }
    }
}
