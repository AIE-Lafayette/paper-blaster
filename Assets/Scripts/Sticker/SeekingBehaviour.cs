using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float _seekRange = 5;

    public float SeekRange
    {
        get => _seekRange;
        set => _seekRange = value;
    }

    public bool InRange
    {
        get => (DistanceFromTarget < _seekRange);
    }

    //Called every frame
    private void Update()
    {
        OwnerMovementBehaviour.MoveDirection = DirectionToTarget;
    }
}
