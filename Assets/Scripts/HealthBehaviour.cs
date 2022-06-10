using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void DeathEventHandler(Object value);

public delegate void HitEventHandler();

public class HealthBehaviour : MonoBehaviour
{
    // The owner's current health
    private int _currentHealth;

    // Takes in the owner's game object as the arg
    private DeathEventHandler _onDeath;

    private HitEventHandler _onHit;

    public DeathEventHandler OnDeath
    {
        get => _onDeath;
        set => _onDeath = value;
    }

    public HitEventHandler OnHit
    {
        get => _onHit;
        set => _onHit = value;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public bool Alive
    {
        get => CurrentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!Alive)
        {
           return;
        }

        _currentHealth -= damageAmount;
    }

    // Called every frame
    private void Update()
    {
        if (!Alive)
        {
            OnDeath.Invoke(gameObject);
        }
    }
}
