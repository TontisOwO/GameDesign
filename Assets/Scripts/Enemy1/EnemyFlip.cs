using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFlip : MonoBehaviour
{
        void OnTriggerEnter2D(Collider2D enemy)
        {
            if (enemy.tag == ("Enemy"))
            {
                enemy.transform.Rotate(0f, 180f, 0f);
            }
        }
    
}
