using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
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
    }

    public void Move()
    {
        if (_rigidbody.velocity.magnitude > MaxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;

        _rigidbody.AddForce(MoveDirection * _moveSpeed * Time.deltaTime);
    }
}
