using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : BulletBehaviour
{
    private GameObject _visual;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 1.5f))
        {
            if (collider.tag == "PaperBall" || collider.tag == "Sticker")
                collider.GetComponent<HealthBehaviour>().TakeDamage(1);
        }

        base.OnTriggerEnter(other);
    }
}
