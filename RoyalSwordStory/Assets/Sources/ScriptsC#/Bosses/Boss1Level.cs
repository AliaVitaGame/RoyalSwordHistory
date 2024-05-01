using UnityEngine;
[RequireComponent(typeof(BossesStats))]
public class Boss1Level : MonoBehaviour
{
    private Transform _playerTransform;
    private BossesStats _stats;
    private float _jumpCooldown = 15f;
    private float _jumpTimer = 0f;
    private bool _isJumping = false;
    private Vector2 _initialPosition;
    private float _restTime = 5f;
    private bool _isResting = false;
    private float _jumpDuration = 0.5f;
    private float _jumpTimerElapsed = 0f;
    private float _pauseBeforeJump = 2f;
    private bool _isPausedBeforeJump = false;
    private float _pauseTimer = 0f;
    private bool _playerInsideAggroRange = false;
    private Animator _bossAnimator;
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _stats = GetComponent<BossesStats>();
        _initialPosition = transform.position;
        _bossAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!_isResting)
        {
            _jumpTimer += Time.deltaTime;

            if (_jumpTimer >= _jumpCooldown && !_isJumping && !_isPausedBeforeJump)
            {
                _isPausedBeforeJump = true;
                _pauseTimer = 0f;
            }

            if (_isPausedBeforeJump)
            {
                _bossAnimator.SetBool("CriticalAttack", false);
                _bossAnimator.SetBool("Idle", true);
                _bossAnimator.SetBool("Attack1", false);
                _bossAnimator.SetBool("Attack2", false);
                _bossAnimator.SetBool("Run", false);
                _pauseTimer += Time.deltaTime;

                if (_pauseTimer >= _pauseBeforeJump)
                {
                    _isPausedBeforeJump = false;
                    JumpToPlayer();
                }
            }
        }

        if (_isJumping)
        {
            _bossAnimator.SetBool("CriticalAttack", true);
            _bossAnimator.SetBool("Idle", false);
            _bossAnimator.SetBool("Attack1", false);
            _bossAnimator.SetBool("Attack2", false);
            _bossAnimator.SetBool("Run", false);
            _jumpTimerElapsed += Time.deltaTime;
            float t = _jumpTimerElapsed / _jumpDuration;
            transform.position = Vector2.Lerp(_initialPosition, _playerTransform.position, t);

            if (t >= 1f)
            {
                _isJumping = false;
                _isResting = true;
                _initialPosition = transform.position;
                StartCoroutine(RestForSeconds(_restTime));
            }
        }

        if (_isResting)
        {
            _bossAnimator.SetBool("CriticalAttack", false);
            _bossAnimator.SetBool("Idle", true);
            _bossAnimator.SetBool("Attack1", false);
            _bossAnimator.SetBool("Attack2", false);
            _bossAnimator.SetBool("Run", false);
        }

        if (!_isJumping && !_isResting && !_isPausedBeforeJump && !_playerInsideAggroRange)
        {
            _bossAnimator.SetBool("CriticalAttack", false);
            _bossAnimator.SetBool("Idle", false);
            _bossAnimator.SetBool("Attack1", false);
            _bossAnimator.SetBool("Attack2", false);
            _bossAnimator.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _stats.MoveSpeed * Time.deltaTime);
        }
        if (_playerInsideAggroRange && !_isResting && !_isPausedBeforeJump)
        {
            _bossAnimator.SetBool("CriticalAttack", false);
            _bossAnimator.SetBool("Idle", false);
            RandomAnimationAttackBoss();
            _bossAnimator.SetBool("Run", false);
        }
        Vector3 direction = _playerTransform.position - transform.position;
    
        float scaleDirection = direction.x < 0 ? -2f : 2f;

        transform.localScale = new Vector3(scaleDirection, transform.localScale.y, transform.localScale.z);
    }
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _stats.AggroRange, LayerMask.GetMask("Player"));
        if (colliders != null && colliders.Length > 0)
        {
            _playerInsideAggroRange = true;
        }
        else
        {
            _playerInsideAggroRange = false;
        }
    }

    private void JumpToPlayer()
    {
        _isJumping = true;
        _jumpTimer = 0f;
        _jumpTimerElapsed = 0f;
        _initialPosition = transform.position;
    }

    private System.Collections.IEnumerator RestForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isResting = false;
    }
    private void RandomAnimationAttackBoss()
    {
        var randomValue = Random.Range(1, 3);
        Debug.Log(randomValue);
        _bossAnimator.SetBool($"Attack{randomValue}", true);
    }
}
