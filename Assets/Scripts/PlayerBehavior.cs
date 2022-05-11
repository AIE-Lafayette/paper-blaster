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

    // Start is called before the first frame update
    void Awake()
    {
        _health = GetComponent<HealthBehavior>();
        _player = GetComponent<Rigidbody>();

        _health.CurrentHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
            onDeath();
    }

    /// <summary>
    /// Called when the player runs out of lives
    /// </summary>
    private void onDeath()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Called when the player collides with some object
    /// </summary>
    /// <param name="other">The other object</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PaperBall")
        {
            //Timer of i-frames
            if (Time.time - _timer < 3)
                return;
            //Takes damage and resets player to the middle
            _health.TakeDamage(1);
            PlayerHealth = _health.CurrentHealth;
            _player.position = new Vector3(0f, .5f, 0f);
            _player.velocity = Vector3.zero;
            //Sets the timer to the current time to reset timer
            _timer = Time.time;
        }

    }
}
