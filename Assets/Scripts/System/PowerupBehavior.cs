using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up * 70f * Time.deltaTime);
        transform.Rotate(Vector3.forward * 70f * Time.deltaTime);
    }

    /// <summary>
    /// Called when something collides with the powerup
    /// </summary>
    /// <param name="other">The other collider that hits the powerup</param>
    private void OnTriggerEnter(Collider other)
    {
        //If the other collider is the player and the player has no active powerup
        if (other.tag == "Player" && !PlayerShootingBehavior.PowerupActive)
        {
            //Set the players current powerup held to be this powerup
            //and destroy this powerup
            PlayerShootingBehavior.CurrentPowerupHeld = tag;
            Destroy(gameObject);

        }
    }
}
