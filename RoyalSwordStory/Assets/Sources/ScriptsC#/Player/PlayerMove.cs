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
    [Space]
    [SerializeField] private AudioClip[] jumpAudios;
    [SerializeField] private AudioFX audioFX;

    private int _currentCountJump;
    private float _startPlayerSpriteScaleX;
    private bool _isStopMove;
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
        if (PlatformManager.IsDesktop)
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
        if (_isStopMove)
        {
            SetVelosity(Vector2.zero);
            return;
        }

        SetVelosity(InputX * speedMove * Time.fixedDeltaTime, _rigidbody.velocity.y);
        RotateSprite();
    }

    public void Jump()
    {
        if (_currentCountJump >= countJump) return;
        if (_isStopMove) return;

        SetVelosity(_rigidbody.velocity.x, jumpForce);
        _currentCountJump++;
        audioFX.PlayAudioRandomPitch(jumpAudios[GetRandomValue(0, jumpAudios.Length)]);
    }

    public void SetHorizontal(float X)
        => InputX = X;

    private void GroundCheck()
    {
        if (_groundChecker.IsGround) _currentCountJump = 0;
    }

    private void Animation()
    {
        if(_isStopMove == false)
        _animationController.MoveAnimation(InputX);
        else
            _animationController.MoveAnimation(0);

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

    private int GetRandomValue(int min, int max) 
        => Random.Range(min, max);

    public void SetVelosity(float X, float Y)
        => _rigidbody.velocity = new Vector2(X, Y);

    public void SetVelosity(Vector2 value)
       => _rigidbody.velocity = value;

    public void SetStopMove(bool stopMove)
        => _isStopMove = stopMove;

    public Vector3 GetVelocity() => _rigidbody.velocity;    
    public bool GetIsGround() => _groundChecker.IsGround;

    public Rigidbody2D GetRigidbody() => _rigidbody;
}
