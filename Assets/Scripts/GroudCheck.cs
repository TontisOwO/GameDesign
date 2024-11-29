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
        if (other.gameObject.CompareTag("Floor"))
        {
            parent.Land();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        parent.FallOff();
    }
}
