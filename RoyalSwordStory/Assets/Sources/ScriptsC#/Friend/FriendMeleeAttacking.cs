using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FriendMove))]
[RequireComponent(typeof(FriendStats))]
public class FriendMeleeAttacking : MonoBehaviour, IUnitAttacking
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
    private FriendMove _friendMove;
    private FriendStats _friendStats;
    private FriendAnimationController _animationController;


    private void OnEnable()
    {
        GetComponent<FriendStats>().FriendStanEvent += SetStopAttacking;
    }

    private void OnDisable()
    {
        GetComponent<FriendStats>().FriendStanEvent -= SetStopAttacking;
    }

    private void Start()
    {
        _friendMove = GetComponent<FriendMove>();
        _friendStats = GetComponent<FriendStats>();
        _animationController = GetComponent<FriendAnimationController>();
    }

    private void FixedUpdate()
    {
        FindTargetCircle();

        if (_target)
        {
            if (Physics2D.OverlapCircle(GetPositionCircle(), radiusDamage, LayerTarget))
                StartAttack();
            else
                _friendMove.MoveToPoint(_target.position);
        }
        else
        {
            _friendMove.MoveToPlayer();
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

        IsAttacking = true;
        StartCoroutine(Swing());
    }

    public IEnumerator Swing()
    {
        if (_isStopAttacking == false)
        {
            _animationController.RandomSwingAnimation();
            _friendMove.SetStopMove(true);

            yield return new WaitForSeconds(swingTime);

            StartCoroutine(Attack());
            StartCoroutine(AttackTimer(attackTime));
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

            for (int i = 0; i < tempTargets.Length; i++)
            {
                if (tempTargets[i].TryGetComponent(out IUnitHealthStats unitHealth))
                {
                    unitHealth.TakeDamage(damage, StunTime, repulsion * transform.localScale.x);
                }
            }
        }
    }

    public IEnumerator AttackTimer(float time)
    {
        IsAttacking = true;

        yield return new WaitForSeconds(time);

        IsAttacking = false;


        if (_isStopAttacking == false)
            _friendMove.SetStopMove(false);

        _animationController.EndetAttack();
    }


    public void SetStopAttacking(bool stopAttacking)
    {
        _isStopAttacking = stopAttacking;
        _friendMove.SetStopMove(stopAttacking);
    }

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
    }
}