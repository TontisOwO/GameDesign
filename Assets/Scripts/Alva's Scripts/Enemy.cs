using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //Attack related
    [SerializeField] Animator enemyAnim;
    float attackTimer;
    float attackTime = 3;
    bool seesPlayer;
    bool TakingDamage;


    //EnemySeek


    GameObject moveTowardsThis;
    GameObject theEnemy;

    //Health
    public int MaxHealth = 100;
    public int CurrentHealth;

    //enemyBehaviour
    public int LifeTotal = 3;
    public int EnemySpeed = 5;
    public Rigidbody2D rb;

    void Start()
    {
        //enemySeek
        moveTowardsThis = GameObject.FindWithTag("Player");

        theEnemy = GameObject.FindWithTag("Enemy");

        //health
        CurrentHealth = MaxHealth;

        //enemyBehaviour
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        

        if (seesPlayer)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                attackTimer = 0;
            }

            if (attackTimer == 0)
            {
                enemyAnim.SetTrigger("Attack");
                attackTimer = attackTime;
            }
        }

        if (TakingDamage)
        {
            enemyAnim.SetTrigger("Damage");
            TakingDamage = false;
            Debug.Log("G");
        }

        //enemySeek
        theEnemy.transform.position = Vector3.MoveTowards(theEnemy.transform.position, moveTowardsThis.transform.position, 0.035f);

        //enemyBehaviour
        // rb.linearVelocity = transform.right * EnemySpeed;
    }

    public void TakeDamage(int damage)
    {
        //enemyHealth
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
        TakingDamage = true;
    }

    private void Die()
    {
        //enemyHealth
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        Debug.Log("Trigger");

        //enemyFlip
        if (enemy.tag == ("Enemy"))
        {
            enemy.transform.Rotate(0f, 180f, 0f);
        }

        if (enemy.gameObject.tag == "Player")
        {
           
            seesPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "Player")
        {
           
            seesPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        }
    }

}

