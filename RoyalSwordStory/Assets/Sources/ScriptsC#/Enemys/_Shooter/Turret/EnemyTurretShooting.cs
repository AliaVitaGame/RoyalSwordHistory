using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyTurretShooting : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float timeStun = 0.3f;
    [SerializeField] private float repulsion = 1;
    [SerializeField] private float aggressionRadius = 15;
    [SerializeField] private float speedMoveBullet = 10;
    [SerializeField] private bool autoGuidanceBullet;
    [SerializeField] private Transform[] shotPoint;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Bullet bulletPrefab;

    private bool _isReload;
    private EnemyAnimationController _animator;

    private void Start()
    {
        _animator = GetComponent<EnemyAnimationController>();
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, aggressionRadius, layerTarget);

        if (target) Shot(target.transform);
    }

    private void Shot(Transform target)
    {
        if (_isReload) return;
        StartCoroutine(Reload());
        var point = shotPoint[Random.Range(0, shotPoint.Length)];
        var tempBullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
        tempBullet.SetStats(damage, speedMoveBullet, timeStun, repulsion, target, transform, autoGuidanceBullet);
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        yield return new WaitForSeconds(reloadTime);
        _isReload = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }
}
