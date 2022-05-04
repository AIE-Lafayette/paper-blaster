using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBehavior
{
    public Camera Camera;
    private Vector3 _movePosition;
    private Rigidbody _rb;
    private bool _thruster;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        MaxSpeed = 20;
        MoveSpeed = 4500;
    }

    private void FixedUpdate()
    {
        LookAtCursor();
    }

    private void LookAtCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            _movePosition = hit.point;
        }
        Debug.Log(_rb.velocity.magnitude);
        Vector3 direction = (_movePosition - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 7.5f);
        Quaternion newRot = transform.rotation;
        newRot.x = 0;
        newRot.z = 0;
        transform.rotation = newRot;
    }

    public void ActivateThruster()
    {
        MoveDirection = transform.forward;
        Move();
    }
}
