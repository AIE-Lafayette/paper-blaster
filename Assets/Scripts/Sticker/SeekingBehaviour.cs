using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBehaviour : SteeringBehaviour
{
    // The range that the owner must be in to seek the target
    [SerializeField]
    private float _seekRange = 5;

    public float SeekRange
    {
        get => _seekRange;
        set => _seekRange = value;
    }

    // Returns whether or not the owner is in range of the target
    public bool InRange
    {
        get => (DistanceFromTarget < _seekRange);
    }

    //Called every frame
    private void Update()
    {
        // Changes the owner's movement direction to be towards the target
        OwnerMovementBehaviour.MoveDirection = DirectionToTarget;
    }
}
