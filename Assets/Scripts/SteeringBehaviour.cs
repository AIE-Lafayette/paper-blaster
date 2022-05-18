using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    //The transform of the behaviour's target
    [SerializeField]
    protected Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    //The owner's movementBehaviour component
    protected MovementBehavior OwnerMovementBehaviour;

    //Returns the direction to the target from the owner
    protected Vector3 DirectionToTarget 
    {
        get => (_target.position - transform.position).normalized;
    }

    //Called when this component is initialized
    private void Awake()
    {
        OwnerMovementBehaviour = GetComponent<MovementBehavior>();
    }

    public void SetTarget(Transform target) 
    {
        _target = target;
    }
}
