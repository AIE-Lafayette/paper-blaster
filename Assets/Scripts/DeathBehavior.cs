using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private bool _particleOnDeath;
    [SerializeField] private GameObject _particleEffect;
    private RoutineBehaviour.TimedAction _deathTimer;

    private void Awake()
    {
        _deathTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => Death(), TimedActionCountType.SCALEDTIME, _timer);
    }

    public void Death() 
    {
        if (_particleOnDeath)
            Instantiate(_particleEffect, transform.position, Quaternion.identity);
        RoutineBehaviour.Instance.StopTimedAction(_deathTimer);
        Destroy(gameObject);
    }
}
