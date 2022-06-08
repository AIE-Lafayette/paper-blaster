using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingBehaviour : SteeringBehaviour
{
    //The radius of the circle that a random point will be generated on
    [SerializeField]
    private float _radius;
    //The distance of said circle from the game object
    [SerializeField]
    private float _distance;

    
    //Called every frame
    private void Update()
    {
        //Generating a random point
        float randomNumber = (Random.Range(-100.0f, 100.0f));
        Vector3 randomPosition = new Vector3(Mathf.Cos(randomNumber), 0, Mathf.Sin(randomNumber));

        //Getting a random point on a circle forward to the game object
        Vector3 circlePosition = transform.position + ( OwnerMovementBehaviour.MoveDirection * _distance);
        Vector3 randomPoint = randomPosition.normalized * _radius;
        randomPoint += circlePosition;

        //Finding the direction of that point from the player's current position
        Vector3 direction = (randomPoint - base.transform.position).normalized;

        //Setting the owner's move direction to be the calculated direction;
        OwnerMovementBehaviour.MoveDirection = direction;
    }
}
