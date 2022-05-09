using UnityEngine;

public class PaperBallMovementBehaviour : MovementBehavior
{
    //Called when this object is added to a scene
    void Start() {
        //Gives the paper ball a random direction and makes applies a force
        MoveDirection = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        Move();
    }

}
