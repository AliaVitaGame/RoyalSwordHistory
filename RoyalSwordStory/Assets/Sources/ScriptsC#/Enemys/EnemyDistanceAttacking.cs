using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyDistanceAttacking : MonoBehaviour, IUnitDistanceAttacking
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float stunTime = 0.3f;
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

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public float ReloadTime
    {
        get => reloadTime;
        set => reloadTime = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public float Repulsion
    {
        get => repulsion;
        set => repulsion = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public float StunTime
    {
        get => stunTime;
        set => stunTime = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public bool IsReload { get; set; }
    public float LifeTimeBullet
    {
        get => lifeTimeBullet;
        set => lifeTimeBullet = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public float SpeedMoveBullet
    {
        get => speedMoveBullet;
        set => speedMoveBullet = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public LayerMask LayerTarget { get; set; }
    public Bullet BulletPrefab { get; set; }

    private float _startScaleX;
    private bool _stopShot;
    private Collider2D _target;
    private EnemyMove _enemyMove;
    private EnemyAnimationController _animator;

    private void OnEnable()
    {
        GetComponent<EnemyStats>().EnemyStanEvent += SetStopShot;
    }

    private void OnDisable()
    {
        GetComponent<EnemyStats>().EnemyStanEvent -= SetStopShot;
    }

    private void Start()
    {
        _animator = GetComponent<EnemyAnimationController>();
        _enemyMove = GetComponent<EnemyMove>();
        _startScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if(_target == null)
        _target = Physics2D.OverlapCircle(transform.position, aggressionRadius, layerTarget);

        if (_target)
        {
            if(Vector2.Distance(transform.position, _target.transform.position) > aggressionRadius)
            {
                if(_stopShot == false)
                _enemyMove.MoveToPoint(_target.transform.position);
            }
            else
            {
                StartShot(_target.transform);
                _enemyMove.MoveToPoint(transform.position);
            }

            if (_isTriggerAnimation == false)
                _animator.AnimationAttack();
        }
        else
        {
            if (_isTriggerAnimation == false)
                _animator.EndetAttack();
        }
    }

    public void StartShot(Transform target)
    {
        if (IsReload) return;
        if (_stopShot) return;
        StartCoroutine(Reload());

        StartCoroutine(Shot(target));
    }

    public IEnumerator Shot(Transform target)
    {
        _enemyMove.SetStopMove(true);

        if (_isTriggerAnimation)
        {
            _animator.AnimationAttack();
            yield return new WaitForSeconds(0.05f);
            _animator.EndetAttack();
        }


        yield return new WaitForSeconds(_delayShot);
        var point = shotPoint[Random.Range(0, shotPoint.Length)];
        var tempBullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
        tempBullet.SetStats(damage, speedMoveBullet, stunTime, repulsion, target, transform, autoGuidanceBullet, lifeTimeBullet);

        _enemyMove.SetStopMove(false);
    }

    public IEnumerator Reload()
    {
        IsReload = true;
        yield return new WaitForSeconds(reloadTime);
        IsReload = false;
    }

    private void SetStopShot(bool stop) => _stopShot = stop;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }
}
