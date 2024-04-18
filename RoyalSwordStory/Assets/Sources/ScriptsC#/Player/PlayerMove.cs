using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAttacking))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private float jumpForce;
    [SerializeField] private int countJump;

    private int _currentCountJump;
    private float _startPlayerSpriteScaleX;
    private Rigidbody2D _rigidbody;
    private PlayerAttacking _attacking;
    private GroundChecker _groundChecker;
    private PlayerAnimationController _animationController;

    public float InputX { get; private set; }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attacking = GetComponent<PlayerAttacking>();
        _animationController = GetComponent<PlayerAnimationController>();
        _groundChecker = GetComponentInChildren<GroundChecker>();   
        _startPlayerSpriteScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if(PlatformManager.IsDesktop)
        SetHorizontal(Input.GetAxisRaw("Horizontal"));

        Move();
        GroundCheck();
        Animation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public void Move()
    {
        if (_attacking.IsAttacking) return;

        SetVelosity(InputX * speedMove * Time.fixedDeltaTime, _rigidbody.velocity.y);
        RotateSprite();
    }

    public void Jump()
    {
        if (_currentCountJump >= countJump) return;
        _groundChecker.IsGround = false;
        SetVelosity(_rigidbody.velocity.x, jumpForce);
        _currentCountJump++;
    }

    public void SetHorizontal(float X) 
        => InputX = X;

    private void GroundCheck()
    {
        if(_groundChecker.IsGround) _currentCountJump = 0;
    }

    private void Animation()
    {
        _animationController.MoveAnimation(InputX);
        _animationController.JumpAnimation(_rigidbody.velocity.y > 0);
        _animationController.FallAnimation(_rigidbody.velocity.y < 0);
    }

    private void RotateSprite()
    {
        float scaleY = transform.localScale.y;
        float scaleZ = 1;
        if (InputX > 0) transform.localScale = new Vector3(_startPlayerSpriteScaleX, scaleY, scaleZ);
        else if (InputX < 0) transform.localScale = new Vector3(-_startPlayerSpriteScaleX, scaleY, scaleZ);
    }

    public void SetVelosity(float X, float Y)
        => _rigidbody.velocity = new Vector2(X, Y);

    public Rigidbody2D GetRigidbody() => _rigidbody;
}
