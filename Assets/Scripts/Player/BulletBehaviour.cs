using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathTimerBehaviour))]
public class BulletBehaviour : MonoBehaviour
{
    private DeathTimerBehaviour _deathTimerBehaviour;
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

    private void Awake()
    {
        _deathTimerBehaviour = GetComponent<DeathTimerBehaviour>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    virtual public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PaperBall":
            {
                other.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
            case "Sticker":
            {
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
            case "Player":
            {
                break;
            }
        }
    }
}
