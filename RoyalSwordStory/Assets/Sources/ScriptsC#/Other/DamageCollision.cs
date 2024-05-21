using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageCollision : MonoBehaviour
{
    [SerializeField] private bool _collisionEnter = true;
    [SerializeField] private bool _collisionStay;

    private float _damage;
    private float _timeStun;
    private float _repulsion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collisionEnter == false) return;

        Damage(collision.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collisionEnter == false) return;

        Damage(collision.transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_collisionStay == false) return;

        Damage(collision.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_collisionStay == false) return;

        Damage(collision.transform);
    }

    public void SetStats(float damage, float timeStun, float repulsion)
    {
        _damage = damage;
        _timeStun = timeStun;
        _repulsion = repulsion;
    }

    private void Damage(Transform target)
    {
        if (target.TryGetComponent(out IUnitHealthStats unit))
            unit.TakeDamage(_damage, _timeStun, _repulsion);
    }

    public void IgnoreCollision(Collider2D collider) 
        => Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
}
