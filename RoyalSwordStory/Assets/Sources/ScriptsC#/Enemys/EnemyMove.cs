using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speedMove = 5;
    [SerializeField] private float jumpForce = 13;
    [Space]
    [SerializeField] private float groundDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [SerializeField] private float stopDistance = 2;
    [SerializeField] private float maxRandomTimeJump = 1;
    [SerializeField] private float minRandomTimeJump = -1;

    private bool _isStopMove;
    private bool _isGround;
    private PlayerMove _player;
    private Rigidbody2D _rigidbody;
    private EnemyAnimationController _animationController;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMove>();
        _animationController = GetComponent<EnemyAnimationController>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        IsGroundCheck();
        MoveToPoint(_player.transform.position);
    }

    public void MoveToPoint(Vector2 point)
    {
        if (_isStopMove) return;

        if (Vector2.Distance(transform.position, point) > stopDistance)
        {
            Vector2 target = point;
            target.y = _isGround ? target.y : transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, target, speedMove * Time.fixedDeltaTime);
            _animationController.MoveAnimation(true);
        }
        else
        {
            _animationController.MoveAnimation(false);
        }

        SetScaleX(point.x > transform.position.x ? 1 : -1);
        Jump(point);

        _animationController.JumpAnimation(_rigidbody.velocity.y > 0 && _isGround == false);
        _animationController.FallAnimation(_rigidbody.velocity.y < 0 && _isGround == false);
    }

    public void Jump(Vector2 targetAttack)
    {
        if (_isGround)
        {
            var randomJumpForce = jumpForce + Random.Range(minRandomTimeJump, maxRandomTimeJump);

            if (targetAttack.y > transform.position.y + 1)
                SetVelocity(new Vector2(_rigidbody.velocity.x, randomJumpForce));
        }
    }

    public void SetVelocity(Vector2 vector)
    {
        _rigidbody.velocity = vector;
    }

    public void SetStopMove(bool isStop) 
        => _isStopMove = isStop;

    public void SetScaleX(float x) 
        => transform.localScale = new Vector2(x, transform.localScale.y);
    public float GetStopDistance() => stopDistance;
    public bool GetIsGround() => _isGround;

    private void IsGroundCheck()
        => _isGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
    }
}
