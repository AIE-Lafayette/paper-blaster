using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathTimerBehaviour))]
public class BulletBehaviour : MonoBehaviour
{
    // Used to delete the bullet after a set time
    private DeathTimerBehaviour _deathTimerBehaviour;

    // The bullet's rigidbody, collider, and health component
    private Rigidbody _rigidbody;
    private SphereCollider _collider;
    private HealthBehaviour _health;

    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    public HealthBehaviour Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public SphereCollider Collider
    {
        get { return _collider; }
        set { _collider = value; }
    }

    // Called when the object is instantiated
    private void Awake()
    {
        // Gets some of the bullet's components
        _deathTimerBehaviour = GetComponent<DeathTimerBehaviour>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    // Called on collision with another collider
    virtual public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            // Deletes both the bullet and object hit
            case "PaperBall":
            case "Sticker":
            {
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
        }
    }
}
