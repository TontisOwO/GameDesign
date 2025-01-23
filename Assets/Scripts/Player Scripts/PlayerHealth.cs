using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    private bool isDead;

    public GameManager gameManager;
    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;

            gameManager.gameOver();

            Debug.Log("Dead");
        }
    }
}