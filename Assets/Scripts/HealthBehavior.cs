using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    private int _currentHealth;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
    }
}
