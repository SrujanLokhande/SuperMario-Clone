using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _RigidBody;
    private Vector2 _velocity;
    private Collider2D _collider;

    private float _inputAxis;
    private bool _hasControl = true;

    private Camera _camera;

    [SerializeField]
    private float _maxJumpHeight = 5f;

    [SerializeField]
    private float _maxJumpTime = 1f;

    private float _jumpForce => (2f * _maxJumpHeight) / (_maxJumpTime / 2f);                    // jump force to go in the vertical direction
    private float _gravity => (-2f * _maxJumpHeight) / Mathf.Pow((_maxJumpTime / 2f), 2);       // gravity to fall down


    private bool _isGrounded { get; set; }

    public bool _isJumping { get; private set; }
    public bool _isRunning => Mathf.Abs(_velocity.x) > 0.25f || Mathf.Abs(_inputAxis) > 0.25f;          // if the mario is moving even slightly
    public bool _isSliding => (_inputAxis > 0f && _velocity.x < 0f) || (_inputAxis < 0f && _velocity.x > 0f);   // if the mario is turning from right to left or left to right

    [SerializeField]
    private float _moveSpeed = 8f;

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();           // gets the player rigidBody
        _collider = GetComponent<Collider2D>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _RigidBody.isKinematic = false;
        _collider.enabled = true;
        _velocity = Vector2.zero;
        _isJumping = false;
    }

    private void OnDisable()                        // making all the components to disable once the game is over
    {
        _RigidBody.isKinematic = true;
        _collider.enabled = false;
        _velocity = Vector2.zero;
        _isJumping = false;
    }

    private void Update()
    {
        HorizontalMovement();        

        _isGrounded = _RigidBody.CircleCast(Vector2.down);

        if (_isGrounded)
        {
            GroundedMovement();
        }
        ApplyGravity();

    }
    private void FixedUpdate()
    {
        Vector2 position = _RigidBody.position;
        position += _velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));       // makes the bounds of the camera based on the screen size

        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);                  // not allowing mario to get out of bounds

        _RigidBody.MovePosition(position);                      // not adding add force because mario has some sort of acceleration and decceleration in movement
    }

    private void HorizontalMovement()
    {
        if (_hasControl)
        {
            _inputAxis = Input.GetAxis("Horizontal");
            _velocity.x = Mathf.MoveTowards(_velocity.x, _inputAxis * _moveSpeed, _moveSpeed * Time.deltaTime);     // player horizontal movement
        }

        if(_RigidBody.CircleCast(Vector2.right * _velocity.x))
        {
            _velocity.x = 0f;                                       // checking if mario hits a wall on his left or right and making the horizontal velocity to be 0
        }

        if(_velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(_velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180, 0);        // making the mario turn left if he is moving left
        }
    }

    private void GroundedMovement()
    {
        _velocity.y = Mathf.Max(_velocity.y, 0f);
        _isJumping = _velocity.y > 0f;
        if(Input.GetButtonDown("Jump"))
        {
            _velocity.y = _jumpForce;                           // adding a jump force in vertical direction
            _isJumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool _isFalling = _velocity.y < 0f || !Input.GetButton("Jump");          // if jump button is not pressed or verticaly velocity is less than 0
        float _multiplier = _isFalling ? 2f : 1f;

        _velocity.y += _gravity * _multiplier * Time.deltaTime;
        _velocity.y = Mathf.Max(_velocity.y, _gravity / 2f);                    // applying terminal velocity for not letting mario fall to fast
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))        // to make mario bounce if he destroys the enemies
        {
            if(transform.DotProduct(collision.transform, Vector2.down))         // dot product to check if mario is hitting the enemies or not
            {
                _velocity.y = _jumpForce / 2f;                                  // making the mario jump
                _isJumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotProduct(collision.transform, Vector2.up))           // if the mario is colliding with a block than making the velocity to 0 to immedietly fall off
            {
                _velocity.y = 0f;               
            }
        }
    }
}
