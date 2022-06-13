using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : BulletBehaviour
{
    private GameObject _visual;

    public override void OnTriggerEnter(Collider other)
     {
        if (other.CompareTag("Player"))
            return;
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 1.5f))
        {
            if (collider.CompareTag("Sticker"))
                collider.GetComponentInParent<HealthBehaviour>().TakeDamage(1);
            else if (collider.CompareTag("PaperBall"))
                collider.GetComponent<HealthBehaviour>().TakeDamage(1);
        }

        base.OnTriggerEnter(other);
    }
}
