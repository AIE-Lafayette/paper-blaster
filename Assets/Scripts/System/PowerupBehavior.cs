using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerShootingBehavior _player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 70f * Time.deltaTime);
    }

    void onPickup()
    {
        RoutineBehaviour.Instance.StartNewTimedAction(args => _player.CurrentPowerup = "Normal", TimedActionCountType.SCALEDTIME, 15);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.CurrentPowerup = tag;
            onPickup();
            Destroy(gameObject);
        }
    }
}
