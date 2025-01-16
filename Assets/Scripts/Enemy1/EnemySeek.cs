using UnityEngine;

public class EnemySeek : MonoBehaviour
{
    GameObject moveTowardsThis;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTowardsThis = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTowardsThis.transform.position, 0.035f);
    }
}
