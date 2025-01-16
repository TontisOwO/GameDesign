using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int MaxHealth = 100;
    public int CurrentHealth;

    
    void Start()
    {
        CurrentHealth = MaxHealth;
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


    
}
