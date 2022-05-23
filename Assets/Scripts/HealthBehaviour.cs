using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void DeathEventHandler(Object value);

public class HealthBehaviour : MonoBehaviour
{
    private int _currentHealth;

    //Takes in the owner's game object as the arg
    private DeathEventHandler _onDeath;

    public DeathEventHandler OnDeath
    {
        get => _onDeath;
        set => _onDeath = value;
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
            OnDeath.Invoke(gameObject);
        }

        _currentHealth -= damageAmount;

        if (!Alive)
        {
            _currentHealth = 0;
            OnDeath.Invoke(gameObject);
        }
    }
}
