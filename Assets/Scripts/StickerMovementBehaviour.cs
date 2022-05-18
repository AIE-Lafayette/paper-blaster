using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerMovementBehaviour : MovementBehavior
{

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
