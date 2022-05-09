using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehavior : MonoBehaviour
{
    private PlayerMovement _playerMovement;

 
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GetThrusterInput();
    }

    void GetThrusterInput()
    {
        if (Input.GetButton("Jump"))
        {
            _playerMovement.ActivateThruster();
        }
    }
}
