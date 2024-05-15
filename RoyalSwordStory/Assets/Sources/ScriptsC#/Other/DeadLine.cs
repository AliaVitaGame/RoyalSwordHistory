using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IUnitHealthStats healthStats))
            healthStats.TakeDamage(healthStats.MaxHealth, 0, 0);
    }
}
