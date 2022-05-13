using UnityEngine;

public class PaperBallMovementBehaviour : MovementBehavior
{
    //The slowest speed that the paper ball can move at
    float _minSpeed;

    //Called when this object is added to a scene
    private void Start()
    {
       
        Move();
    }

    public override void Move()
    {
        //Gets the paper ball's rigidbody and calls the base move function
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        base.Move();

        //Clamps the paper ball's speed
        if (rigidbody.velocity.magnitude < _minSpeed)
        {
            rigidbody.velocity = (rigidbody.velocity.normalized * _minSpeed);
        }
    }

}
