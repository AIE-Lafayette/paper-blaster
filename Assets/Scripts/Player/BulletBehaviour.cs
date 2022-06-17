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

    [SerializeField]
    private int _damage;

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

    public int Damage { get => _damage; set => _damage = value; }

    // Called when the object is instantiated
    virtual protected void Awake()
    {
        _damage = 1;
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
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(_damage);
                _deathTimerBehaviour.Kill();
                break;
            }
        }
    }
}
