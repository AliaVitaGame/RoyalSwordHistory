using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
public class EnemyMeleeAttacking : MonoBehaviour, IUnitAttacking
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackTime = 2;
    [SerializeField] private float repulsion = 10;
    [SerializeField] private float stunTime = 0.3f;
    [SerializeField] private float radiusDamage = 1.73f;
    [SerializeField] private float swingTime = 0.5f;
    [SerializeField] private float aggressionRadius = 7;
    [SerializeField] private float attackRadius = 7;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 distanceDamage;
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

    private Transform _target;
    private EnemyMove _enemyMove;
    private EnemyAnimationController _animationController;


    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _animationController = GetComponent<EnemyAnimationController>();
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            if (GetDistance(transform.position, _target.position) < attackRadius)
                StartAttack();
            else
                _enemyMove.MoveToPoint(_target.position);
        }
        else
            FindTargetCircle();
    }

    private void FindTargetCircle()
    {
        _target = Physics2D.OverlapCircle(transform.position, aggressionRadius, LayerTarget).transform;
    }

    public void StartAttack()
    {
        if (IsAttacking) return;

        IsAttacking = true;
        StartCoroutine(Swing());
    }

    public IEnumerator Swing()
    {
        yield return new WaitForSeconds(swingTime);
        _animationController.RandomSwingAnimation();
        StartCoroutine(Attack());
        StartCoroutine(AttackTimer(attackTime));
    }

    public IEnumerator Attack()
    {
        _animationController.AnimationAttack();

        yield return null;

        var positionCircle = (transform.position + (Vector3)distanceDamage) + (transform.localScale.x * Vector3.right);
        var tempTargets = Physics2D.OverlapCircleAll(positionCircle, radiusDamage, layerTarget);

        for (int i = 0; i < tempTargets.Length; i++)
        {
            if (tempTargets[i].TryGetComponent(out IUnitHealthStats unitHealth))
            {
                unitHealth.TakeDamage(damage, StunTime);
            }
        }
    }

    public IEnumerator AttackTimer(float time)
    {
        IsAttacking = true;

        yield return new WaitForSeconds(time);

        IsAttacking = false;

        _animationController.EndetAttack();
    }


    private float GetDistance(Vector2 a, Vector2 b)
        => Vector2.Distance(a, b);

    private void OnDrawGizmosSelected()
    {
        if (IsAttacking) Gizmos.color = Color.red;
        else Gizmos.color = Color.yellow;

        var positionCircle = (transform.position + (Vector3)distanceDamage) + (transform.localScale.x * Vector3.right);
        Gizmos.DrawWireSphere(positionCircle, radiusDamage);
    }
}