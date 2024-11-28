using UnityEngine;

public class GroudCheck : MonoBehaviour
{
    [SerializeField] Movement parent;
    private void Awake()
    {
        parent = GetComponentInParent<Movement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ha gay");
        if (other.gameObject.CompareTag("Floor"))
        {
            parent.Land();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        parent.FallOf();
        Debug.Log("lamo");
    }
}
