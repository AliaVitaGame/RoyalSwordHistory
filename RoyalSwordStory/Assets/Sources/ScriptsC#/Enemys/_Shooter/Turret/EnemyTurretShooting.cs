using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyTurretShooting : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float timeStun = 0.3f;
    [SerializeField] private float lifeTimeBullet = 7;
    [SerializeField] private float repulsion = 1;
    [SerializeField] private float aggressionRadius = 15;
    [SerializeField] private float speedMoveBullet = 10;
    [SerializeField] private bool autoGuidanceBullet;
    [SerializeField] private Transform[] shotPoint;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Bullet bulletPrefab;
    [Space]
    [SerializeField] private float _delayShot;
    [SerializeField] private bool _isTriggerAnimation;

    private bool _isReload;
    private float _startScaleX;
    private EnemyAnimationController _animator;

    private void Start()
    {
        _animator = GetComponent<EnemyAnimationController>();
        _startScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, aggressionRadius, layerTarget);

        if (target)
        {
            StartShot(target.transform);
            SetRotation(target.transform.position.x > transform.position.x);

            if (_isTriggerAnimation == false)
                _animator.AnimationAttack();
        }
        else
        {
            if (_isTriggerAnimation == false)
                _animator.EndetAttack();
        }
    }

    private void StartShot(Transform target)
    {
        if (_isReload) return;
        StartCoroutine(Reload());

        StartCoroutine(Shot(target));
    }

    private IEnumerator Shot(Transform target)
    {
        if (_isTriggerAnimation)
        {
            _animator.AnimationAttack();
            yield return new WaitForSeconds(0.05f);
            _animator.EndetAttack();
        }


        yield return new WaitForSeconds(_delayShot);
        var point = shotPoint[Random.Range(0, shotPoint.Length)];
        var tempBullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
        tempBullet.SetStats(damage, speedMoveBullet, timeStun, repulsion, target, transform, autoGuidanceBullet, lifeTimeBullet);
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        yield return new WaitForSeconds(reloadTime);
        _isReload = false;
    }

    private void SetRotation(bool isRight)
    {
        var scale = transform.localScale;
        var X = isRight ? _startScaleX : _startScaleX * -1;
        transform.localScale = new Vector3(X, scale.y, scale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }
}
