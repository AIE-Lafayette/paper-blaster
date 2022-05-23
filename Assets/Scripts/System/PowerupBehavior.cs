using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerShootingBehavior _player;
    private string _currentPowerupHeld;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 70f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.CurrentPowerupHeld = tag;
            Destroy(gameObject);
        }
    }
}
