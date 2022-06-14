using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    //Variables for the player to take damage on collision
    private HealthBehaviour _health;
    private Rigidbody _player;
    public static int PlayerHealth = 4;

    //Variables for what needs to happen when the player gets hit
    private RoutineBehaviour.TimedAction _iframesTimer;
    [SerializeField]private Renderer _renderer;
    [SerializeField]private BoxCollider _collider;
    private bool _iframesActive;
    [SerializeField]
    private AudioSource _takeDamageSound;

    // Start is called before the first frame update
    void Awake()
    {
        //The health and player are set
        _health = GetComponent<HealthBehaviour>();
        _player = GetComponent<Rigidbody>();

        //Sets the current health to 3
        _health.CurrentHealth = 3;
        //Creates a bade routine behavior for the timer for invincibility frames
        _iframesTimer = new RoutineBehaviour.TimedAction();
        //When the player dies the game over scene is loaded and the player is destroyed
        _health.OnDeath = (gameObject) => {
            PlayerPrefs.SetInt("Score", GameManagerBehavior.Score);
            SceneManager.LoadScene("game_over_scene");
            Destroy(gameObject);
        };
    }

    // Update is called once per frame
    void Update()
    {
        //If the iframes timer isn't active, create a new routine behavior to set it to be active again
        //to make the player flash
        if (!_iframesTimer.IsActive)
            _iframesTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, 0.15f);
    }

    /// <summary>
    /// Updates the visual of the player so the iframe timer makes the player flash
    /// </summary>
    void UpdateVisual() 
    {
        //if the iframes are active set the renderer to be disabled
        if (_iframesActive)
        {
            _renderer.enabled = !_renderer.enabled;
            //_collider.enabled = !_collider.enabled;
        }
    }

    /// <summary>
    /// Called when the player is hit by an enemy or paper ball
    /// </summary>
    public void OnHit()
    {
        //If the iframes aren't active go through the function
        if (!_iframesActive)
        {
            //Takes damage and resets player to the middle
            _health.TakeDamage(1);
            PlayerHealth = _health.CurrentHealth;
            _takeDamageSound.Play();
            _player.position = new Vector3(11.25f, 0.5f, 6.25f);
            _player.velocity = Vector3.zero;

            //Timer of i-frames
            _iframesActive = true;
            RoutineBehaviour.Instance.StartNewTimedAction(args => IframeReset(), TimedActionCountType.SCALEDTIME, 1.6f);
            _iframesTimer = RoutineBehaviour.Instance.StartNewTimedAction(args => UpdateVisual(), TimedActionCountType.SCALEDTIME, .15f);
        }
    }

    /// <summary>
    /// Resets the iframe timer to create flashing
    /// </summary>
    void IframeReset() 
    {
        _iframesActive = false;
        RoutineBehaviour.Instance.StopTimedAction(_iframesTimer);
        _renderer.enabled = true;
        //_collider.enabled = true;
    }
}
