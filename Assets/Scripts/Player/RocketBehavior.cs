using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : BulletBehaviour
{
    //The visual of the rocket
    private GameObject _visual;

    [SerializeField]
    private HealthBehaviour _healthBehaviour;

    protected override void Awake()
    {
        Damage = 3;
        _healthBehaviour.CurrentHealth = 1;
        _healthBehaviour.OnDeath = (gameObject) =>
        {
            //For every collider in a sphere around the rocket after it collides...
            foreach (Collider collider in Physics.OverlapSphere(transform.position, 1.5f))
            {
                //...yhe objects take damage
                if (collider.CompareTag("Sticker"))
                    collider.GetComponentInParent<HealthBehaviour>().TakeDamage(1);
                else if (collider.CompareTag("PaperBall"))
                    collider.GetComponent<HealthBehaviour>().TakeDamage(1);
            }
        };
        _healthBehaviour.OnDeath += Destroy;

        base.Awake();
    }

    /// <summary>
    /// Called when the rocket collides with anything
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerEnter(Collider other)
     {
        //Return if the rocket collides with the player
        if (other.CompareTag("Player"))
            return;

        _healthBehaviour.TakeDamage(1);

        base.OnTriggerEnter(other);
    }
}
