using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimerBehaviour : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private bool _particleOnDeath;
    [SerializeField] private GameObject _particleEffect;
    private RoutineBehaviour.TimedAction _deathTimer;

    //When the object is instanced in scene wait for the time then run the kill function
    private void Awake()
    {
        _deathTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => Kill(), TimedActionCountType.SCALEDTIME, _timer);
    }

    //Destroy the object but check to spawn particles first.
    public void Kill() 
    {
        if (_particleOnDeath)
            Instantiate(_particleEffect, transform.position, Quaternion.identity);
        RoutineBehaviour.Instance.StopTimedAction(_deathTimer);
        Destroy(gameObject);
    }
}
