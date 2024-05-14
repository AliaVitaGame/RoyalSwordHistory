using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadLine : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IUnitHealthStats healthStats))
            healthStats.TakeDamage(healthStats.MaxHealth, 0, 0);
    }
}
