using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(FriendAnimationController))]
public class SupportUnitMove : MonoBehaviour
{
    [SerializeField] private float speedMove = 20;
    [SerializeField] private float speedMove�losely = 5;
    [SerializeField] private float jumpForce = 13;
    [Space]
    [SerializeField] private float groundDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [SerializeField] private float stopDistance = 2;
    [SerializeField] private float stopDistanceForPlayer = 5;

    private float speed;
    private bool _isStopMove;
    private bool _isGround;
    private Rigidbody2D _rigidbody;
    private PlayerMove _playerMove;
    private FriendAnimationController _animationController;
    private CapsuleCollider2D _collider;


    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
      
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<FriendAnimationController>();
        _rigidbody.freezeRotation = true;

        _collider = GetComponent<CapsuleCollider2D>();

        _playerMove = FindObjectOfType<PlayerMove>();
        speed = speedMove;
    }

    private void FixedUpdate()
    {
        IsGroundCheck();
        MoveToPlayer();
    }

    public void MoveToPoint(Vector2 point)
    {
        if (_isStopMove)
        {
            _animationController.MoveAnimation(false);
            return;
        }

        if (Vector2.Distance(transform.position, point) > stopDistance)
        {
            Vector2 target = point;
            target.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            _animationController.MoveAnimation(true);
        }
        else
        {
            _animationController.MoveAnimation(false);
        }

        speed = speedMove;
        SetScaleX(point.x > transform.position.x ? 1 : -1);
        Jump(point);

        JumpAnimation();
    }

    public void MoveToPlayer()
    {
        var player = _playerMove.transform.position;
        var distance = Vector2.Distance(transform.position, player);

        speed = distance >= stopDistanceForPlayer * 1.1f ? speedMove : speedMove�losely;

        if (distance > stopDistanceForPlayer)
        {
            MoveToPoint(player);
        }
        else
        {
            _animationController.MoveAnimation(false);
            JumpAnimation();
        }
    }

    public void Jump(Vector2 targetAttack)
    {
        if (_isGround && _isStopMove == false)
        {
            var randomJumpForce = jumpForce;

            if (targetAttack.y > transform.position.y + (_collider.offset.y + _collider.size.y + _collider.size.x))
                SetVelocity(new Vector2(_rigidbody.velocity.x, randomJumpForce));
        }
    }

    private void JumpAnimation()
    {
        _animationController.JumpAnimation(_rigidbody.velocity.y > 0 && _isGround == false);
        _animationController.FallAnimation(_rigidbody.velocity.y < 0 && _isGround == false);
    }

    public void SetVelocity(float x, float y)
        => _rigidbody.velocity = new Vector3(x, y);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
    }
}
