using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
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
    [SerializeField] private float timePlayAttackDownFX = 30;
    [SerializeField] private DamageCollision damageCollision;
    [SerializeField] private AudioClip[] attackDownAudio;
    [SerializeField] private ParticleSystem attackDownFX;
    [SerializeField] private Button attackButtonDown;
    [Space]
    [SerializeField] private float forceAttackUpForJump = 3;
    [Space]
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private Vector2 distanceDamage = Vector2.right * 0.5f;
    [Space]
    [SerializeField] private ParticleSystem damageEffect;
    [SerializeField] private AudioFX audioFX;
    [SerializeField] private AudioClip[] audiosAttack;

    private bool _isOpenUI;
    private PlayerStats _playerStats;
    private PlayerAnimationController _animationController;
    private PlayerMove _playerMove;

    public System.Action DamageCollisionEvent;

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

    private void OnEnable()
    {
        ManagerUI.OpenUIEvent += SetOpenUI;
    }

    private void OnDisable()
    {
        ManagerUI.OpenUIEvent -= SetOpenUI;
    }

    private void Start()
    {
        if (gameObject.TryGetComponent(out PlayerMove playerMove))
            _playerMove = playerMove;

        _playerStats = GetComponent<PlayerStats>();
        _animationController = GetComponent<PlayerAnimationController>();
        attackDownFX.Stop();

        damageCollision.SetStats(damageAttackDown, stunTime, repulsion, layerTarget, _playerStats);
        damageCollision.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && PlatformManager.IsDesktop)
            StartAttack();

        if (Input.GetMouseButtonDown(1) && PlatformManager.IsDesktop)
            StartAttack(true);
    }

    private void FixedUpdate()
    {
        RefreshUI();
    }

    public void StartAttack(bool attackDown = false)
    {
        if (IsAttacking) return;
        if (_isOpenUI) return;
        if (_playerStats.IsDead) return;

        IsAttacking = true;

        if (attackDown && _playerMove.GetIsGround() == false)
        {
            StartCoroutine(AttackDown());
        }
        else
        {
            StartCoroutine(Attack());
            StartCoroutine(AttackTimer(AttackTime));
        }
    }

    public IEnumerator AttackDown()
    {
        IsAttacking = true;
        _playerMove.SetStopMove(true);

        int timeAttackDown = 0;

        while (_playerMove.GetIsGround() == false)
        {
            _playerMove.SetVelosity((Vector2.down + Vector2.right * transform.localScale.x) * speedAttackDown);
            _animationController.FallAnimation(true);

            timeAttackDown++;
            if (timeAttackDown == timePlayAttackDownFX)
            {
                attackDownFX.Stop();
                attackDownFX.Play();
                damageCollision.gameObject.SetActive(true);
                audioFX.PlayAudioRandomPitch(attackDownAudio[Random.Range(0, attackDownAudio.Length)]);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
        _playerMove.SetStopMove(false);

        if (_animationController)
            _animationController.EndetAttack();

        IsAttacking = false;
        damageCollision.gameObject.SetActive(false);
        attackDownFX.Stop();
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

        bool damageOne = false;

        for (int i = 0; i < tempTargets.Length; i++)
        {
            if (tempTargets[i].TryGetComponent(out IUnitHealthStats unitHealth))
            {
                unitHealth.TakeDamage(damage, StunTime, repulsion * transform.localScale.x);
                damageEffect.transform.position = tempTargets[i].transform.position;
                damageEffect.Play();

                if (_playerMove.GetIsGround() == false)
                    _playerMove.SetVelosity(new Vector2(transform.localScale.x, 1) * forceAttackUpForJump);

                if (damageOne == false)
                {
                    DamageCollisionEvent?.Invoke();
                    damageOne = true;
                }

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

    private void RefreshUI()
    {
        if (attackButtonDown)
            attackButtonDown.gameObject.SetActive(_playerMove.GetIsGround() == false);
    }

    private Vector3 GetPositionCircle()
     => (transform.position + new Vector3(transform.localScale.x * distanceDamage.x, distanceDamage.y));

    private void SetOpenUI(bool value) => _isOpenUI = value;

    private void OnDrawGizmosSelected()
    {
        if (IsAttacking) Gizmos.color = Color.red;
        else Gizmos.color = Color.yellow;

        var positionCircle = GetPositionCircle();
        Gizmos.DrawWireSphere(positionCircle, radiusDamage);
    }
}