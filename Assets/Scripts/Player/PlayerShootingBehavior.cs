using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackSpeed;
    private bool _readyToAttack;

    private string _currentPowerup;

    public string CurrentPowerup
    {
        get { return _currentPowerup; }
        set { _currentPowerup = value; }
    }

    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    void Start()
    {
        _readyToAttack = true;
    }

    public void Shoot() 
    {
        if (_readyToAttack) 
        {
            if (_currentPowerup == "TripleShotPowerup")
            {
                _readyToAttack = false;
                Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(bullet.transform.forward * 1000);
                Rigidbody bullet2 = Instantiate(_projectile, _bulletPoint.position, angleChange).GetComponent<Rigidbody>();

                //bullet2.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 30, 0), Time.deltaTime);
                bullet2.AddForce(bullet2.transform.forward * 1000);
                RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
            }
            else
            {
                _readyToAttack = false;
                Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(bullet.transform.forward * 1000);
                RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
            }
            
        }
    }
}
