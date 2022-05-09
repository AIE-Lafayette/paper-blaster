using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    private Camera _cam;
    private float _buffer;
    private float _leftBorder;
    private float _rightBorder;
    private float _bottomBorder;
    private float _topBorder;
    private float _distanceY;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _moveSpeed;

    public Vector3 MoveDirection
    {
        get { return _moveDirection; }
        set { _moveDirection = value; }
    }

    public float Buffer
    {
        get { return _buffer; }
        set { _buffer = value; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _cam = Camera.main;

        _distanceY = Mathf.Abs(_cam.transform.position.y + transform.position.y);

        _leftBorder = _cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, _distanceY)).x;
        _rightBorder = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, _distanceY)).x;
        
        _topBorder = _cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, _distanceY)).z;
        _bottomBorder = -_topBorder;

        Buffer = 0.5f;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (transform.position.x < _leftBorder - Buffer)
        {
            transform.position = new Vector3(_rightBorder + Buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x > _rightBorder + Buffer)
        {
            transform.position = new Vector3(_leftBorder - Buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.z < _bottomBorder - Buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _topBorder + Buffer);
        }
        if (transform.position.z > _topBorder + Buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _bottomBorder - Buffer);
        }
    }

    public void Move()
    {
        if (_rigidbody.velocity.magnitude > MaxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;

        _rigidbody.AddForce(MoveDirection * _moveSpeed * Time.deltaTime);
    }
}
