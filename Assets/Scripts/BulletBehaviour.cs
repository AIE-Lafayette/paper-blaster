using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private DeathTimerBehaviour _deathTimerBehaviour;
    private Rigidbody _rigidbody;
    private Collider _collider;
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

    private void Awake()
    {
        _deathTimerBehaviour = GetComponent<DeathTimerBehaviour>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _health = GetComponent<HealthBehaviour>();
        _health.OnDeath = delegate (Object args) { _deathTimerBehaviour.Kill(); };
        Health.CurrentHealth = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PaperBall":
            {
                other.GetComponent<HealthBehaviour>().TakeDamage(1);
                Health.TakeDamage(1);
                break;
            }
            case "Sticker":
            {
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(1);
                Health.TakeDamage(1);
                break;
            }
            case "Player":
            {
                break;
            }
            case "Sticker":
            {
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
        }
        
    }

}
