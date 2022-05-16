using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    //The transform of the behaviour's target
    [SerializeField]
    private Transform _target;

    //The force that the behaviour will have
    [SerializeField]
    private float _force;

    //The force that the behaviour will have
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    //The owner's movementBehaviour component
    private MovementBehavior _ownerMovementBehaviour;

    //The owner's movementBehaviour component
    public MovementBehavior OwnerMovementBehaviour
    {
        get => _ownerMovementBehaviour;
    }

    //Returns the direction to the target from the owner
    public Vector3 DirectionToTarget 
    {
        get => (_target.position - transform.position).normalized;
    }

    //Called when this component is initialized
    private void Awake()
    {
        _ownerMovementBehaviour = GetComponent<MovementBehavior>();
    }
}
