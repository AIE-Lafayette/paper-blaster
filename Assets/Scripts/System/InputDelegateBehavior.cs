using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegateBehavior : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerMovement _playerMovement;
    private PlayerShootingBehavior _playerShooting;
    private bool _activateShooting;
    [SerializeField]
    private PowerupBehavior _powerup;

    // Sets the player components
    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShootingBehavior>();
    }

    /// <summary>
    /// Called when the players controls are enabled
    /// </summary>
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    /// <summary>
    /// Called when the players controls are disabled
    /// </summary>
    private void OnDisable()
    {
        _playerControls.Disable();
    }

    /// <summary>
    /// Adds to the delegates of the player controls to make sure they have the right functionality
    /// </summary>
    void Start()
    {
        //Movement started and canceled turns on and off the thruster respectively
        _playerControls.Ship.Movement.started += (InputAction.CallbackContext context) =>
            { _playerMovement.ActivateThruster(); _playerMovement.ThrusterOn = true; };

        _playerControls.Ship.Movement.canceled += (InputAction.CallbackContext context) =>
        { _playerMovement.ThrusterOn = false; };

        //Shoot started shoots the projectile
        _playerControls.Ship.Shoot.started += (InputAction.CallbackContext context) =>
            { _playerShooting.Shoot(); _activateShooting = true; };

        _playerControls.Ship.Shoot.canceled += (InputAction.CallbackContext context) =>
        { _activateShooting = false; };

        //Activate powerup started calls the powerups on activate function
        _playerControls.Ship.ActivatePowerup.started += (InputAction.CallbackContext context) =>
            _playerShooting.onActivate();

        //The pause started pauses the game in its current state
        _playerControls.Ship.Pause.started += (InputAction.CallbackContext context) =>
            GameManagerBehavior.PauseGame();
    }

    /// <summary>
    /// If the thruster is on, activate the thruster.
    /// This is called for the renderer and the thruster sound to work proplerly
    /// </summary>
    private void Update()
    {
        if (_playerMovement.ThrusterOn)
            _playerMovement.ActivateThruster();
        if (_activateShooting)
            _playerShooting.Shoot();
    }

}
