using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private DeathTimerBehaviour _deathTimerBehaviour;

    private void Awake()
    {
        _deathTimerBehaviour = GetComponent<DeathTimerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PaperBall":
            {
                other.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
            case "Sticker":
            {
                other.attachedRigidbody.GetComponent<HealthBehaviour>().TakeDamage(1);
                _deathTimerBehaviour.Kill();
                break;
            }
        }
    }
}
