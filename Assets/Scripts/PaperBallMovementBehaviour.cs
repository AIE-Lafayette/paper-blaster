using UnityEngine;

public class PaperBallMovementBehaviour : MovementBehavior
{
    //Called when this object is added to a scene
    private void Start()
    {
        Move();
    }

    public override void Move()
    {
        //Gives the ball a random direction
        MoveDirection = new Vector3(Random.Range(-500.0f, 500.0f), 0, Random.Range(-500.0f, 500.0f)).normalized;

        base.Move();
    }

}
