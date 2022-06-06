using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerMovementBehaviour : MovementBehavior
{
    //Called when the component is added to the scene
    private void Start()
    {
         transform.forward = new Vector3(Random.Range(-500.0f, 500.0f), 0, Random.Range(-500.0f, 500.0f)).normalized;
    }

    override public void Move()
    {
        Rigidbody.AddForce(MoveDirection * MoveSpeed * Time.deltaTime);

                    //If the object is moving above the max speed
        if (Rigidbody.velocity.magnitude > MaxSpeed)
            //Set the velocity to be the max speed
            Rigidbody.velocity = Rigidbody.velocity.normalized * MaxSpeed;
    }

    //Called every frame
    override protected void Update()
    {
        Move();
        base.Update();
    }
}
