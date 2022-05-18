using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackSpeed;
    private bool _readyToAttack;

    void Start()
    {
        _readyToAttack = true;
    }

    public void Shoot() 
    {
        if (_readyToAttack) 
        {
            _readyToAttack = false;
            Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
            bullet.AddForce(bullet.transform.forward * 1000);
            RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
        }
    }
}
