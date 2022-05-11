using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private HealthBehavior _health;
    private Rigidbody _player;

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

    private void onDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PaperBall")
        {
            _health.CurrentHealth--;
            _player.position = new Vector3(0f, .5f, 0f);
            _player.velocity = Vector3.zero;
        }

    }
}
