using UnityEngine;

public class EnemyTurretMove : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float aggressionRadius = 15;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 stopDistance = new Vector2(7, 2.5f);

    private Collider2D _target;
    private void FixedUpdate()
    {

        if (_target)
            MoveToPoint(_target.transform);
        else
            _target = Physics2D.OverlapCircle(transform.position, aggressionRadius, layerTarget);
    }

    private void MoveToPoint(Transform point)
    {
        if (point == null) return;

        var distance = stopDistance;
        distance.x *= point.localScale.x;

        if (Vector2.Distance(transform.position, point.position) > stopDistance.x)
            transform.position = Vector3.MoveTowards(transform.position, point.position + (Vector3)distance, speed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }
}
