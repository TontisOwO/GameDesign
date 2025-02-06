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


    //EnemySeek

  
          GameObject moveTowardsThis;

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

        //health
        CurrentHealth = MaxHealth;

        //enemyBehaviour
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    void Update()
    {

        attackTimer -= Time.deltaTime;

        if(attackTimer < 0)
        {
            attackTimer = 0;
        }

        if(attackTimer == 0)
        {
            enemyAnim.SetTrigger("Attack");
            attackTimer = attackTime;
        }

        //enemySeek
        transform.position = Vector3.MoveTowards(transform.position, moveTowardsThis.transform.position, 0.035f);

        //enemyBehaviour
        rb.linearVelocity = transform.right * EnemySpeed;




    }

    public void TakeDamage(int damage)
    {
        //enemyHealth
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //enemyHealth
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        //enemyFlip
        if (enemy.tag == ("Enemy"))
        {
            enemy.transform.Rotate(0f, 180f, 0f);
        }
    }


}

