using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speedMove = 5;
    [SerializeField] private float jumpForce = 10;
    [Space]
    [SerializeField] private float groundDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [SerializeField] private float stopDistance = 2;
    [SerializeField] private float maxRandomTimeJump = 1;
    [SerializeField] private float minRandomTimeJump = -1;

    private PlayerMove _player;
    private bool _isGround;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMove>();
    }

    private void FixedUpdate()
    {
        IsGroundCheck();
        MoveToPoint(_player.transform.position);
    }

    private void Update()
    {

    }

    public void MoveToPoint(Vector2 point)
    {
        if (Vector2.Distance(transform.position, point) > stopDistance)
        {
            Vector2 target = point;
            target.y = _isGround ? target.y : transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, target, speedMove * Time.fixedDeltaTime);
        }

        SetScaleX(point.x > transform.position.x ? 1 : -1);
        Jump(point);
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

    private void IsGroundCheck()
        => _isGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

    public void SetScaleX(float x) 
        => transform.localScale = new Vector2(x, transform.localScale.y);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
    }
}
