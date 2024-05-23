using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    [SerializeField] private float radiusDamage;

    private float _damage;
    private float _timeStun;
    private float _repulsion;
    private LayerMask _layerDamage;

    private IUnitHealthStats _myHealth;


    private void FixedUpdate()
    {
        var collisionObject = Physics2D.OverlapCircle(transform.position, radiusDamage, _layerDamage);

        if(collisionObject)
        Damage(collisionObject.transform);
    }

    public void SetStats(float damage, float timeStun, float repulsion, LayerMask layerDamage, IUnitHealthStats ignoreCollision = null)
    {
        _damage = damage;
        _timeStun = timeStun;
        _repulsion = repulsion;
        _layerDamage = layerDamage;

        if (ignoreCollision != null)
            IgnoreCollision(ignoreCollision);
    }

    private void Damage(Transform target)
    {
        if (target.TryGetComponent(out IUnitHealthStats unit))
        {
            if (_myHealth != null)
            {
                if (_myHealth == unit) return;
            }
          
            unit.TakeDamage(_damage, _timeStun, _repulsion);
        }
    }

    public void IgnoreCollision(IUnitHealthStats unityStats)
        => _myHealth = unityStats;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusDamage);
    }
}
