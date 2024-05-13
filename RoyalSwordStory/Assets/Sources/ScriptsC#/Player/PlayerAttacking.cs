using System.Collections;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour, IUnitAttacking
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackTime = 0.12f;
    [SerializeField] private float forceAttack = 7;
    [SerializeField] private float repulsion = 10;
    [SerializeField] private float stunTime = 0.3f;
    [SerializeField] private float radiusDamage = 1.73f;
    [Space]
    [SerializeField] private float damageAttackDown = 10;
    [SerializeField] private float speedAttackDown = 35;
    [Space]
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 distanceDamage = Vector2.right * 0.5f;
    [Space]
    [SerializeField] private ParticleSystem damageEffect;
    [SerializeField] private AudioFX audioFX;
    [SerializeField] private AudioClip[] audiosAttack;

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

        IsAttacking = true;

        if (_playerMove.GetIsGround())
        {
            StartCoroutine(Attack());
            StartCoroutine(AttackTimer(AttackTime));
        }
        else
        {
            StartCoroutine(AttackDown());
        }


    }

    public IEnumerator AttackDown()
    {
        IsAttacking = true;
        _playerMove.SetStopMove(true);

        while (_playerMove.GetIsGround() == false)
        {
            _playerMove.SetVelosity((Vector2.down + Vector2.right * transform.localScale.x) * speedAttackDown);
            _animationController.FallAnimation(true);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _playerMove.SetStopMove(false);

        if (_animationController)
            _animationController.EndetAttack();

        IsAttacking = false;

    }

    public IEnumerator Attack()
    {
        yield return null;

        if (_animationController)
            _animationController.RandomAnimationAttack();

        audioFX.PlayAudioRandomPitch(audiosAttack[Random.Range(0, audiosAttack.Length)]);

        if (_playerMove)
            _playerMove.SetVelosity(forceAttack * transform.localScale.x, _playerMove.GetRigidbody().velocity.y);

        var positionCircle = GetPositionCircle();
        var tempTargets = Physics2D.OverlapCircleAll(positionCircle, radiusDamage, layerTarget);

        for (int i = 0; i < tempTargets.Length; i++)
        {
            if (tempTargets[i].TryGetComponent(out IUnitHealthStats unitHealth))
            {
                unitHealth.TakeDamage(damage, StunTime, repulsion * transform.localScale.x);
                damageEffect.transform.position = tempTargets[i].transform.position;
                damageEffect.Play();
            }
        }
    }

    public IEnumerator AttackTimer(float time)
    {
        IsAttacking = true;

        yield return new WaitForSeconds(time);

        if (_animationController)
            _animationController.EndetAttack();

        IsAttacking = false;
    }

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