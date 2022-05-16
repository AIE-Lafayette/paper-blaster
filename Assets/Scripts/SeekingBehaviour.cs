using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBehaviour : SteeringBehaviour
{
    //Called every frame
    private void Update()
    {
        OwnerMovementBehaviour.MoveDirection = DirectionToTarget;
    }
}
