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

    //The owner's movementBehaviour component
    private MovementBehavior _movementBehaviour;

    //Returns the direction to the target from the owner
    private Vector3 DirectionToTarget 
    {
        get 
        {
            return (_target.position - transform.position).normalized;
        }
    }

    //Called when this component is initialized
    private void Awake()
    {
        _movementBehaviour = GetComponent<MovementBehavior>();
    }
}
