using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speedMove = 5;
    [SerializeField] private float jumpForce = 15;
    [Space]
    [SerializeField] private float groundDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [SerializeField] private float stopDistance = 2;
    [SerializeField] private float minRandomTimeJump = 1;
    [SerializeField] private float maxRandomTimeJump = -1;

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
        transform.position = Vector2.MoveTowards(transform.position, point, speedMove * Time.fixedDeltaTime);

        Jump(point);
    }

    public void Jump(Vector2 targetAttack)
    {
        if (_isGround)
        {
            var randomJumpForce = jumpForce + Random.Range(minRandomTimeJump, maxRandomTimeJump);

            if (targetAttack.y > transform.position.y)
                SetVelocity(new Vector2(_rigidbody.velocity.x, randomJumpForce));
            else
                SetVelocity(new Vector2(_rigidbody.velocity.x, -randomJumpForce));
        }
    }

    public void SetVelocity(Vector2 vector)
    {
        _rigidbody.velocity = vector;
    }

    private void IsGroundCheck()
        => _isGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
    }
}
