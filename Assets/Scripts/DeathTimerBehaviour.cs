using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimerBehaviour : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private bool _particleOnDeath;
    [SerializeField] private GameObject _particleEffect;
    private RoutineBehaviour.TimedAction _deathTimer;

    private void Awake()
    {
        _deathTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => Kill(), TimedActionCountType.SCALEDTIME, _timer);
    }

    public void Kill() 
    {
        if (_particleOnDeath)
            Instantiate(_particleEffect, transform.position, Quaternion.identity);
        RoutineBehaviour.Instance.StopTimedAction(_deathTimer);
        Destroy(gameObject);
    }
}
