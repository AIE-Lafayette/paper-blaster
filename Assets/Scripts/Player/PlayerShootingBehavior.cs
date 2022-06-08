using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingBehavior : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _projectile;
    [SerializeField] private RocketBehavior _rocketProjectile;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackSpeed;
    private bool _readyToAttack;
    public AudioSource[] Sounds;
    private AudioSource _normalShot;
    private AudioSource _laserShot;
    private AudioSource _rocketShot;

    private string _currentPowerup = "Normal";
    private static string _currentPowerupHeld = "Normal";

    public static string CurrentPowerupHeld
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
        Sounds = GetComponents<AudioSource>();
        _normalShot = Sounds[0];
        _laserShot = Sounds[1];
        _rocketShot = Sounds[3];
    }

    public void onActivate()
    {
        if (_currentPowerupHeld == "Normal")
            return;
        _currentPowerup = _currentPowerupHeld;

        RoutineBehaviour.Instance.StartNewTimedAction(args => _currentPowerup = "Normal", TimedActionCountType.SCALEDTIME, 15);
    }

    public void Shoot() 
    {
        if (_readyToAttack && !GameManagerBehavior.pauseCheck) 
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
                _readyToAttack = false;
                GameObject bullet = Instantiate(_rocketProjectile.gameObject, _bulletPoint.position, _bulletPoint.rotation);
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + 2, bullet.transform.localScale.y + 2, bullet.transform.localScale.z + 2);
                RocketBehavior bulletBehavior = bullet.GetComponent<RocketBehavior>();
                bulletBehavior.Rigidbody.AddForce(bullet.transform.forward * 700);
                RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed * 2.1f);
                _rocketShot.Play();
            }
            if (_currentPowerup == "Normal")
            {
                _readyToAttack = false;
                Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(bullet.transform.forward * 1000);
                RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
                _normalShot.Play();
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
        _laserShot.Play();
        Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * .5f, bullet.transform.localScale.y, bullet.transform.localScale.z * 4);
        bullet.AddForce(bullet.transform.forward * 2000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed * .2f);
    }
}
