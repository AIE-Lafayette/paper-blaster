using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    //The transform of the behaviour's target
    [SerializeField]
    private Transform _target;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    //The owner's movementBehaviour component
    protected MovementBehavior OwnerMovementBehaviour;

    //Returns the direction to the target from the owner
    public Vector3 DirectionToTarget 
    {
        get => (_target.position - transform.position).normalized;
    }

    //Returns the distance from the owner to its target
    public float DistanceFromTarget
    {
        get => (_target.position - transform.position).magnitude;
    }

    //Called when this component is initialized
    private void Awake()
    {
        OwnerMovementBehaviour = GetComponent<MovementBehavior>();
    }
}
