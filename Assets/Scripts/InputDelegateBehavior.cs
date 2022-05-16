using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegateBehavior : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerMovement _playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    // Update is called once per frame
    void Start()
    {
        _playerControls.Ship.Movement.started += (InputAction.CallbackContext context) =>
             _playerMovement.ActivateThruster();
    }
}
