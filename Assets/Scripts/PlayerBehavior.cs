using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private HealthBehavior _health;
    private Rigidbody _player;
    private float _timer = 0;
    public static int PlayerHealth = 3;
    private RoutineBehaviour.TimedAction _iframesAction;
    [SerializeField]private MeshRenderer _renderer;

    private bool _iframesCheck;
    private bool _iframesCheckCheck;

    // Start is called before the first frame update
    void Awake()
    {
        _health = GetComponent<HealthBehavior>();
        _player = GetComponent<Rigidbody>();

        _health.CurrentHealth = 3;
        //_renderer = GetComponent<MeshRenderer>();
        _iframesAction = new RoutineBehaviour.TimedAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
            OnDeath();
        if (!_iframesAction.IsActive)
            _iframesAction = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, 0.5f);
    }

    void UpdateVisual() 
    {
        if (_iframesCheckCheck)
        {
            _iframesCheck = !_iframesCheck;
            if (_iframesCheck)
                _renderer.enabled = false;
            if (!_iframesCheck)
                _renderer.enabled = true;
        }
    }

    /// <summary>
    /// Called when the player runs out of lives
    /// </summary>
    private void OnDeath()
    {
        Destroy(gameObject);
    }

    public void OnHit()
    {
        if (!_iframesCheckCheck)
        {
            //Takes damage and resets player to the middle
            _health.TakeDamage(1);
            PlayerHealth = _health.CurrentHealth;
            _player.position = new Vector3(11.25f, .5f, 6.25f);
            _player.velocity = Vector3.zero;
            //Sets the timer to the current time to reset timer
            _timer = Time.time;
        }
        //Timer of i-frames
        _iframesCheckCheck = true;
        RoutineBehaviour.Instance.StartNewTimedAction(args => _iframesCheckCheck = false, TimedActionCountType.SCALEDTIME, 3);
        _iframesAction = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, 0.5f);
        RoutineBehaviour.Instance.StopTimedAction(_iframesAction);
    }
}
