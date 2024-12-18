using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyTurretShooting : MonoBehaviour, IUnitDistanceAttacking
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

    public void StartShot(Transform target)
    {
        if (IsReload) return;
        StartCoroutine(Reload());

        StartCoroutine(Shot(target));
    }

    public IEnumerator Shot(Transform target)
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
        tempBullet.SetStats(damage, speedMoveBullet, stunTime, repulsion, target, transform, autoGuidanceBullet, lifeTimeBullet);
    }

    public IEnumerator Reload()
    {
        IsReload = true;
        yield return new WaitForSeconds(reloadTime);
        IsReload = false;
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
