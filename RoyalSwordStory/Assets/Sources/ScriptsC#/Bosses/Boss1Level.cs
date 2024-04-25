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

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _stats = GetComponent<BossesStats>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (!_isResting)
        {
            _jumpTimer += Time.deltaTime;

            if (_jumpTimer >= _jumpCooldown && !_isJumping)
            {
                JumpToPlayer();
            }
        }

        if (_isJumping)
        {
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
            // Ничего не делаем
        }

        if (!_isJumping && !_isResting)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _stats.MoveSpeed * Time.deltaTime);
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
}
