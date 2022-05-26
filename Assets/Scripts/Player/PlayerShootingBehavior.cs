using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackSpeed;
    private bool _readyToAttack;

    private string _currentPowerup = "Normal";
    private string _currentPowerupHeld;

    public string CurrentPowerupHeld
    {
        get { return _currentPowerupHeld ; }
        set { _currentPowerupHeld = value; }
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

    public void onActivate()
    {
        _currentPowerup = _currentPowerupHeld;

        RoutineBehaviour.Instance.StartNewTimedAction(args => _currentPowerup = "Normal", TimedActionCountType.SCALEDTIME, 15);
    }

    public void Shoot() 
    {
        if (_readyToAttack) 
        {
            if (_currentPowerup == "TripleShotPowerup")
            {
                ShootTripleShot();
            }
            if (_currentPowerup == "LaserPowerup")
            {
                ShootLaser();
            }
            if (_currentPowerup == "RocketPowerup")
            {
                _readyToAttack = true;
                Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + 2, bullet.transform.localScale.y + 2, bullet.transform.localScale.z + 2);
                bullet.gameObject.AddComponent<HealthBehaviour>().CurrentHealth = 5;
                bullet.AddForce(bullet.transform.forward * 750);
            }
            if (_currentPowerup == "Normal")
            {
                _readyToAttack = false;
                Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(bullet.transform.forward * 1000);
                RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
            }
            
        }
    }

    private void ShootTripleShot()
    {
        Quaternion angleChange2 = Quaternion.Euler(_bulletPoint.eulerAngles.x, _bulletPoint.eulerAngles.y + 20, _bulletPoint.eulerAngles.z);
        Quaternion angleChange3 = Quaternion.Euler(_bulletPoint.eulerAngles.x, _bulletPoint.eulerAngles.y - 20, _bulletPoint.eulerAngles.z);

        _readyToAttack = false;
        Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        bullet.AddForce(bullet.transform.forward * 1000);
        Rigidbody bullet2 = Instantiate(_projectile, _bulletPoint.position, angleChange2).GetComponent<Rigidbody>();
        bullet2.AddForce(bullet2.transform.forward * 1000);
        Rigidbody bullet3 = Instantiate(_projectile, _bulletPoint.position, angleChange3).GetComponent<Rigidbody>();
        bullet3.AddForce(bullet3.transform.forward * 1000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
    }

    private void ShootLaser()
    {
        _readyToAttack = false;
        Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * .5f, bullet.transform.localScale.y, bullet.transform.localScale.z * 4);
        bullet.AddForce(bullet.transform.forward * 2000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed * .2f);
    }
}
