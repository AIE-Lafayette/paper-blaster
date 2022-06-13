using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    //The variables needed for the respawn border
    private Camera _cam;
    private float _leftBorder;
    private float _rightBorder;
    private float _bottomBorder;
    private float _topBorder;
    private float _distanceY;

    //The variables needed to actually move the game object
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _xBuffer;
    [SerializeField]
    private float _ZBuffer;

    /// <summary>
    /// The direction this object will move
    /// </summary>
    public Vector3 MoveDirection
    {
        get { return _moveDirection; }
        set { _moveDirection = value; }
    }

    /// <summary>
    /// The buffer outside of the camera border so the object can go off-screen then respawn 
    /// off screen on the other side of the screen
    /// </summary>
    public float XBuffer
    {
        get { return _xBuffer; }
        set { _xBuffer = value; }
    }

    /// <summary>
    /// The buffer outside of the camera border so the object can go off-screen then respawn 
    /// off screen on the other side of the screen
    /// </summary>
    public float ZBuffer
    {
        get { return _ZBuffer; }
        set { _ZBuffer = value; }
    }

    /// <summary>
    /// The cap of the speed of this object
    /// </summary>
    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    /// <summary>
    /// The constant speed of movement of this object
    /// </summary>
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

    /// <summary>
    /// Before the game starts set the rigidbody, camera, and the borders for the respawns
    /// </summary>
    private void Awake()
    {
        //Get the rigidbody and camera
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _cam = Camera.main;

        //The distance between the camera and this object
        _distanceY = Mathf.Abs(_cam.transform.position.y + transform.position.y);

        //Set the left and right border by using the camera view and the width of the screen
        //as the original border
        _leftBorder = _cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, _distanceY)).x;
        _rightBorder = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, _distanceY)).x;

        //Set the top and bottom border by using the camera view and the height of the screen
        //as the original border
        _topBorder = _cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, _distanceY)).z;
        _bottomBorder = _cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, _distanceY)).z;

        //This is the basic buffer size for all objects, but can be changed in their specific classes
        XBuffer = -1.1f;
        ZBuffer = -.4f;
    }

    /// <summary>
    /// Update checks for if this object is ever outside of the border, then respawns the object 
    /// at the correct location
    /// </summary>
    virtual protected void Update()
    {
        //If the object is too far to the left
        if (transform.position.x < _leftBorder - XBuffer)
        {
            //spawn on the right side with the same y and z axis
            transform.position = new Vector3((_rightBorder + XBuffer) - .01f, transform.position.y, transform.position.z);
        }
        //If the object is too far to the right
        if (transform.position.x > _rightBorder + XBuffer)
        {
            //spawn on the left side with the same y and z axis
            transform.position = new Vector3((_leftBorder - XBuffer) + .01f, transform.position.y, transform.position.z);
        }
        //If the object is too far to the bottom
        if (transform.position.z < _bottomBorder - ZBuffer)
        {
            //spawn on the top side with the same x and y axis
            transform.position = new Vector3(transform.position.x, transform.position.y, (_topBorder + ZBuffer) - .01f);
        }
        //If the object is too far to the top
        if (transform.position.z > _topBorder + ZBuffer)
        {
            //spawn the object on the top side with the same x and y axis
            transform.position = new Vector3(transform.position.x, transform.position.y, (_bottomBorder - ZBuffer) + .01f);
        }

                       //If the object is moving above the max speed
        if (_rigidbody.velocity.magnitude > MaxSpeed)
            //Set the velocity to be the max speed
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;
            
    }

    /// <summary>
    /// Called when any object needs to move
    /// </summary>
    virtual public void Move()
    {
        //Move in the correct direction scaled up by the move speed
        _rigidbody.AddForce(MoveDirection * _moveSpeed, ForceMode.Impulse);
    }

}
