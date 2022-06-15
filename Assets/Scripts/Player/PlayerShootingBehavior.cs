using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingBehavior : MonoBehaviour
{
    //Serialized fields for the projectiles and attack speed
    [SerializeField] private BulletBehaviour _projectile;
    [SerializeField] private RocketBehavior _rocketProjectile;
    [SerializeField] private BulletBehaviour _laserProjectile;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackSpeed;

    //Variable for player being ready to attack
    private bool _readyToAttack;

    //Variables for the sounds of the projectiles
    public AudioSource[] Sounds;
    private AudioSource _normalShot;
    private AudioSource _laserShot;
    private AudioSource _rocketShot;

    //Variables for the current powerups
    private string _currentPowerup = "Normal";
    private static string _currentPowerupHeld = "Normal";
    public static bool PowerupActive;

    /// <summary>
    /// The tag of the current powerup held
    /// </summary>
    public static string CurrentPowerupHeld
    {
        get { return _currentPowerupHeld ; }
        set { _currentPowerupHeld = value; }
    }

    /// <summary>
    /// The speed between when a bullet can be fired after another
    /// </summary>
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    /// <summary>
    /// Sets all of the components, sounds, and powerups to be defualt
    /// </summary>
    void Start()
    {
        _readyToAttack = true;
        Sounds = GetComponents<AudioSource>();
        CurrentPowerupHeld = "Normal";
        _normalShot = Sounds[0];
        _laserShot = Sounds[1];
        _rocketShot = Sounds[3];
    }

    /// <summary>
    /// Called when the powerup held is activated
    /// </summary>
    public void onActivate()
    {
        //If the player doesn't have a powerup, return
        if (_currentPowerupHeld == "Normal")
            return;

        //Set the current powerup to be the one that is held and set the powerup active
        _currentPowerup = _currentPowerupHeld;
        PowerupActive = true;

        //Start a new timer so after 15 seconds the powerup gets set back to the normal one
        RoutineBehaviour.Instance.StartNewTimedAction(args => { _currentPowerup = "Normal"; CurrentPowerupHeld = "Normal"; PowerupActive = false; }, TimedActionCountType.SCALEDTIME, 15);
    }

    /// <summary>
    /// Called when the player shoots the weapon
    /// </summary>
    public void Shoot() 
    {
        //If the player is ready to attack and the game isn't paused
        if (_readyToAttack && !GameManagerBehavior.pauseCheck) 
        {
            //Check for each powerup tag and fire the correct prjectile based off the tag
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
                ShootRocket();
            }
            if (_currentPowerup == "Normal")
            {
                ShootNormal();
            }
        }
    }

    /// <summary>
    /// Shoots three bullets all angled slightly differently
    /// </summary>
    private void ShootTripleShot()
    {
        //The angles of the two bullets beside the main one
        Quaternion angleChange2 = Quaternion.Euler(_bulletPoint.eulerAngles.x, _bulletPoint.eulerAngles.y + 20, _bulletPoint.eulerAngles.z);
        Quaternion angleChange3 = Quaternion.Euler(_bulletPoint.eulerAngles.x, _bulletPoint.eulerAngles.y - 20, _bulletPoint.eulerAngles.z);

        //Instantiates three bullets and adds force to them
        //Sets a new timer to set ready to attack to be true after a set time
        _readyToAttack = false;
        Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        bullet.AddForce(bullet.transform.forward * 1000);
        Rigidbody bullet2 = Instantiate(_projectile, _bulletPoint.position, angleChange2).GetComponent<Rigidbody>();
        bullet2.AddForce(bullet2.transform.forward * 1000);
        Rigidbody bullet3 = Instantiate(_projectile, _bulletPoint.position, angleChange3).GetComponent<Rigidbody>();
        bullet3.AddForce(bullet3.transform.forward * 1000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
        _normalShot.Play();
    }

    /// <summary>
    /// Fires a bullet that is very quick and precise
    /// </summary>
    private void ShootLaser()
    {
        //Instantiates a new bullet that is faster than an original bullet 
        //Sets a new timer to set ready to attack to be true after a set time
        _readyToAttack = false;
        _laserShot.Play();
        Rigidbody bullet = Instantiate(_laserProjectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        //bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * .5f, bullet.transform.localScale.y, bullet.transform.localScale.z * 4);
        bullet.AddForce(bullet.transform.forward * 2000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed * .6f);
    }

    /// <summary>
    /// Fires a rocket the explodes on impact
    /// </summary>
    private void ShootRocket()
    {
        //Instantiates a rocket and fires it
        //Sets a new timer to set ready to attack to be true after a set time
        _readyToAttack = false;
        GameObject bullet = Instantiate(_rocketProjectile.gameObject, _bulletPoint.position, _bulletPoint.rotation);
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + 2, bullet.transform.localScale.y + 2, bullet.transform.localScale.z + 2);
        RocketBehavior bulletBehavior = bullet.GetComponent<RocketBehavior>();
        bulletBehavior.Rigidbody.AddForce(bullet.transform.forward * 700);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed * 1.5f);
        _rocketShot.Play();
    }

    /// <summary>
    /// Fires the regular shot for the player
    /// </summary>
    private void ShootNormal()
    {
        //Shoots a regular bullet at the regular set speed
        //Sets a new timer to set ready to attack to be true after a set time
        _readyToAttack = false;
        _normalShot.Play();
        Rigidbody bullet = Instantiate(_projectile, _bulletPoint.position, _bulletPoint.rotation).GetComponent<Rigidbody>();
        bullet.AddForce(bullet.transform.forward * 1000);
        RoutineBehaviour.Instance.StartNewTimedAction(args => _readyToAttack = true, TimedActionCountType.SCALEDTIME, _attackSpeed);
        
    }
}
