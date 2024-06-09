using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyMeleeAttacking : MonoBehaviour, IUnitMeleeAttacking
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackTime = 0.7f;
    [SerializeField] private float repulsion = 10;
    [SerializeField] private float stunTime = 0.3f;
    [SerializeField] private float radiusDamage = 1.73f;
    [SerializeField] private float swingTime = 0.5f;
    [SerializeField] private float aggressionRadius = 7;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 distanceDamage = Vector2.right * 1.5f;
    [Space]
    [SerializeField] private AudioFX audioFX;
    [SerializeField] private AudioClip[] audiosAttack;
    public float Damage
    {
        get => damage;
        set => damage = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public float AttackTime
    {
        get => attackTime;
        set => attackTime = Mathf.Clamp(value, 0.15f, 1);
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
    public float RadiusDamage
    {
        get => radiusDamage;
        set => radiusDamage = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public bool IsAttacking
    {
        get;
        set;
    }
    public LayerMask LayerTarget
    {
        get => layerTarget;
        set => layerTarget = value;
    }
    public Vector2 DistanceDamage
    {
        get => distanceDamage;
        set => distanceDamage = value;
    }

    private bool _isStopAttacking;
    private Transform _target;
    private EnemyMove _enemyMove;
    private EnemyStats _enemyStats;
    private EnemyAnimationController _animationController;


    private void OnEnable()
    {
        GetComponent<EnemyStats>().EnemyStanEvent += SetStopAttacking;
    }

    private void OnDisable()
    {
        GetComponent<EnemyStats>().EnemyStanEvent -= SetStopAttacking;
    }

    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _enemyStats = GetComponent<EnemyStats>();
        _animationController = GetComponent<EnemyAnimationController>();
    }

    private void FixedUpdate()
    {
        FindTargetCircle();

        if (_target)
        {
            if (Physics2D.OverlapCircle(GetPositionCircle(), radiusDamage, LayerTarget))
                StartAttack();
            else
                _enemyMove.MoveToPoint(_target.position);
        }
    }

    private void FindTargetCircle()
    {
        var tempObject = Physics2D.OverlapCircle(transform.position, aggressionRadius, LayerTarget);
        if (tempObject)
        {
            if (tempObject.TryGetComponent(out IUnitHealthStats unit))
            {
                if (unit.IsDead == false)
                    _target = tempObject.transform;
            }
        }
    }

    public void StartAttack(bool attackDown = false)
    {
        if (IsAttacking) return;
        if (_isStopAttacking) return;
        if (_enemyMove.GetIsGround() == false) return;

        StartCoroutine(Swing());
    }

    public IEnumerator Swing()
    {
        if (_isStopAttacking == false)
        {
            IsAttacking = true;

            _animationController.RandomSwingAnimation();
            _enemyMove.SetStopMove(true);

            yield return new WaitForSeconds(swingTime);

            if (_isStopAttacking == false)
            {
                StartCoroutine(Attack());
                StartCoroutine(AttackTimer(attackTime));
            }
            else
            {
                IsAttacking = false;
            }
        }
    }

    public IEnumerator Attack()
    {
        if (_isStopAttacking == false)
        {
            _animationController.AnimationAttack();

            audioFX.PlayAudioRandomPitch(audiosAttack[Random.Range(0, audiosAttack.Length)]);

            yield return null;

            var tempTargets = Physics2D.OverlapCircleAll(GetPositionCircle(), radiusDamage, layerTarget);

            if (_isStopAttacking == false)
            {
                for (int i = 0; i < tempTargets.Length; i++)
                {
                    if (tempTargets[i].TryGetComponent(out IUnitHealthStats unitHealth))
                    {
                        if (_isStopAttacking == false)
                            unitHealth.TakeDamage(damage, StunTime, repulsion * transform.localScale.x);
                    }
                }
            }
            else
            {
                _animationController.EndetAttack();
            }


        }
    }

    public IEnumerator AttackTimer(float time)
    {
        IsAttacking = true;

        yield return new WaitForSeconds(time);

        IsAttacking = false;

        if(_isStopAttacking == false)
        _enemyMove.SetStopMove(false);

        _animationController.EndetAttack();
    }


    public void SetStopAttacking(bool stopAttacking)
    {
        _isStopAttacking = stopAttacking;
        _enemyMove.SetStopMove(stopAttacking);

        if (stopAttacking)
        {
            _animationController.MoveAnimation(false);
            _animationController.EndetAttack();
        }
    }

    public void SetAggressionRadius(float value) 
        => aggressionRadius = value;

    private float GetDistance(Vector3 a, Vector3 b)
        => Vector3.Distance(a, b);

    private Vector3 GetPositionCircle()
        => (transform.position + new Vector3(transform.localScale.x * distanceDamage.x, distanceDamage.y));

    private void OnDrawGizmosSelected()
    {
        if (IsAttacking) Gizmos.color = Color.red;
        else Gizmos.color = Color.yellow;

        var positionCircle = GetPositionCircle();
        Gizmos.DrawWireSphere(positionCircle, radiusDamage);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggressionRadius);
    }
}