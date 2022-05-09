using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBehavior
{
    //Variables for getting the camera, move position and rigidbody of the player
    private Camera _camera;
    private Vector3 _movePosition;
    private Rigidbody _rb;

    /// <summary>
    /// On start set the max speed and the move speed
    /// Get the rigidbody from the player and the main camera
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        MaxSpeed = 20;
        MoveSpeed = 4500;
        _camera = Camera.main;
    }

    /// <summary>
    /// Call the function to look at the cursor every frame
    /// </summary>
    private void FixedUpdate()
    {
        LookAtCursor();
    }

    /// <summary>
    /// The player will turn its forward to look at where the cursor is on the screen
    /// </summary>
    private void LookAtCursor()
    {
        //Call for raycasting to get where the cursor actually is on the game screen
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //If the cast is true an the mouse is on the screen
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Move position is set to the position of the cursor
            _movePosition = hit.point;
        }

        //Set the direction to look and rotate to that direction
        Vector3 direction = (_movePosition - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 7.5f);

        //Set the new rotation without the x or z to only rotate on the y axis
        Quaternion newRot = transform.rotation;
        newRot.x = 0;
        newRot.z = 0;
        //Rotate to the new rotation
        transform.rotation = newRot;
    }

    /// <summary>
    /// When spacebar is pressed the thruster will activate, causing the player to move
    /// </summary>
    public void ActivateThruster()
    {
        MoveDirection = transform.forward;
        Move();
    }
}
