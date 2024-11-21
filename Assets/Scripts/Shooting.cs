using Mono.Cecil;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform spawnPos;
    public GameObject bullet;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, spawnPos.position, spawnPos.rotation);
        }
    }
}
