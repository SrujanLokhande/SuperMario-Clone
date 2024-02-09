using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D _RigidBody;
    private Vector2 _enemyVelocity;

    [SerializeField]
    public float _speed = 1f;

    [SerializeField]
    public Vector2 _direction = Vector2.left;


    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()                              // only start to move when they are in focus of camera
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()                         // when they are in focus they start moving which is done using unity's inbuilt function
    {
        _RigidBody.WakeUp();
    }

    private void OnDisable()                                // on disable the velocity becomes zero
    {
        _RigidBody.velocity = Vector2.zero;
        _RigidBody.Sleep();            
    }

    private void FixedUpdate()                                          // making the entites move
    {
        _enemyVelocity.x = _direction.x * _speed;
        _enemyVelocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        _RigidBody.MovePosition(_RigidBody.position + _enemyVelocity * Time.fixedDeltaTime);     // multiplying two times because gravity is m/s^2 

        if(_RigidBody.CircleCast(_direction))
        {
            _direction = -_direction;                                               // making the entites move in opposite direction when they hit something
        }

        if(_RigidBody.CircleCast(Vector2.down))
        {
            _enemyVelocity.y = Mathf.Max(_enemyVelocity.y, 0f);
        }

        if(_direction.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (_direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
