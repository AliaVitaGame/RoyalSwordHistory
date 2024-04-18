using System.Collections;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour, IUnitAttacking
{
    [SerializeField] private float damage = 10;
    [Range(0.15f, 1), SerializeField] private float attackTime = 0.1f;
    [SerializeField] private float forceAttack = 7;
    [SerializeField] private float repulsion = 10;
    [SerializeField] private float stunTime = 0.3f;
    [SerializeField] private float radiusDamage = 1.73f;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 distanceDamage;

    private PlayerAnimationController _animationController;
    private PlayerMove _playerMove;

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
    public float ForceAttack
    {
        get => forceAttack;
        set => forceAttack = Mathf.Clamp(value, 0, Mathf.Infinity);
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


    private void Start()
    {
        if (gameObject.TryGetComponent(out PlayerMove playerMove))
            _playerMove = playerMove;

        _animationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && PlatformManager.IsDesktop)
            StartAttack();
    }

    public void StartAttack()
    {
        if (IsAttacking) return;

        StartCoroutine(Attack());
        StartCoroutine(AttackTimer());
    }

    public IEnumerator Attack()
    {
        yield return null;

        if (_playerMove)
            _playerMove.SetVelosity(forceAttack * transform.localScale.x, _playerMove.GetRigidbody().velocity.y);

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

    public IEnumerator AttackTimer()
    {
        IsAttacking = true;

        if (_animationController)
            _animationController.RandomAnimationAttack();

        yield return new WaitForSeconds(AttackTime);

        if (_animationController)
            _animationController.EndetAttack();

        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (IsAttacking) Gizmos.color = Color.red;
        else Gizmos.color = Color.yellow;

        var positionCircle = (transform.position + (Vector3)distanceDamage) + (transform.localScale.x * Vector3.right);
        Gizmos.DrawWireSphere(positionCircle, radiusDamage);
    }
}