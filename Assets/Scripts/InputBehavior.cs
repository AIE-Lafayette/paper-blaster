using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehavior : MonoBehaviour
{
    //Player movement behavior to activate the thruster
    private PlayerMovement _playerMovement;

     /// <summary>
     /// On awake get the player movement component from this object
     /// </summary>
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    /// Gets the input every frame
    void Update()
    {
        GetInput();
    }

    /// Checks if the space bar is pressed, if so activate the thruster of the player
    void GetInput()
    {
        //if (Input.GetButton("Jump"))
        //{
        //    _playerMovement.ActivateThruster();
        //}
    }
}
