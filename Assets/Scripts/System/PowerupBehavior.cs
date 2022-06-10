using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up * 70f * Time.deltaTime);
        transform.Rotate(Vector3.right * 70f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !PlayerShootingBehavior.PowerupActive)
        {
            PlayerShootingBehavior.CurrentPowerupHeld = tag;
            Destroy(gameObject);
        }
    }
}
