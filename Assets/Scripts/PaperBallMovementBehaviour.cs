using UnityEngine;

public class PaperBallMovementBehaviour : MovementBehavior
{
    private Vector3 _previousDirection;
    
    //The direction that the previous Paper Ball was moving in
    public Vector3 PreviousDirection { get => _previousDirection; set => _previousDirection = value; }


    //Called when this object is added to a scene
    void Start()
    {
        //If this paper ball was assigned with a previous direction...
        if (PreviousDirection.z != 0 && PreviousDirection.x != 0) 
        {
            //Offset that direction and make that the new move direction
            Vector3 offset = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)).normalized;
            MoveDirection = (PreviousDirection + offset).normalized;
        }
        else
        {
            //Otherwise, give the paper ball a random direction
            MoveDirection = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized;
        }

        Move();
    }

}
