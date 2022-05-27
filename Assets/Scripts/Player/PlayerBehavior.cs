using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private HealthBehaviour _health;
    private Rigidbody _player;
    public static int PlayerHealth = 3;
    private RoutineBehaviour.TimedAction _iframesTimer;
    [SerializeField]private Renderer _renderer;
    [SerializeField]private BoxCollider _collider;
    private bool _iframesActive;

    // Start is called before the first frame update
    void Awake()
    {
        _health = GetComponent<HealthBehaviour>();
        _player = GetComponent<Rigidbody>();

        _health.CurrentHealth = 3;
        //_renderer = GetComponent<MeshRenderer>();
        _iframesTimer = new RoutineBehaviour.TimedAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
            OnDeath();
        if (!_iframesTimer.IsActive)
            _iframesTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, 0.15f);
    }

    void UpdateVisual() 
    {
        if (_iframesActive)
        {
            _renderer.enabled = !_renderer.enabled;
            //_collider.enabled = !_collider.enabled;
        }
    }

    /// <summary>
    /// Called when the player runs out of lives
    /// </summary>
    private void OnDeath()
    {
        Destroy(gameObject);
        Application.LoadLevel("game_over_scene");
    }

    public void OnHit()
    {
        if (!_iframesActive)
        {
            //Takes damage and resets player to the middle
            _health.TakeDamage(1);
            PlayerHealth = _health.CurrentHealth;
            _player.position = new Vector3(11.25f, 0.5f, 6.25f);
            _player.velocity = Vector3.zero;

            //Timer of i-frames
            _iframesActive = true;
            RoutineBehaviour.Instance.StartNewTimedAction(args => IframeReset(), TimedActionCountType.SCALEDTIME, 1.6f);
            _iframesTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, .15f);
        }
    }

    void IframeReset() 
    {
        _iframesActive = false;
        RoutineBehaviour.Instance.StopTimedAction(_iframesTimer);
        _renderer.enabled = true;
        //_collider.enabled = true;
    }
}
