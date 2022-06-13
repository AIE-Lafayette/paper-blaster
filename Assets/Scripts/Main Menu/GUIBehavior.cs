using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBehavior : MonoBehaviour
{
    //References
    [SerializeField] private HealthBehaviour _playerHealth;
    [SerializeField] private GameManagerBehavior _gameManager;

    // UI Elements
    [SerializeField] private Image _health01;
    [SerializeField] private Image _health02;
    [SerializeField] private Image _health03;
    [SerializeField] private Image _power01;
    [SerializeField] private Image _power02;
    [SerializeField] private Image _power03;
    [SerializeField] private Text _pageText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Color _deactiveColor;
    [SerializeField] private Color _activeColor;

    void Update()
    {
        //Constantly update the texts
        _scoreText.text = "Score: " + GameManagerBehavior.Score;
        _pageText.text = "Page: " + _gameManager.Page;

        //Change the UI images based on player health
        if (_playerHealth.CurrentHealth == 3)
            FullHP();
        if (_playerHealth.CurrentHealth == 2)
            MediumHP();
        if (_playerHealth.CurrentHealth == 1)
            LowHp();

        //Change the Power up image based on the current power up
        if (PlayerShootingBehavior.CurrentPowerupHeld == "Normal")
            Normal();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "LaserPowerup")
            Laser();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "RocketPowerup")
            Rocket();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "TripleShotPowerup")
            Triple();

        //If the power up is active, change the color of the power up image.
        if (PlayerShootingBehavior.PowerupActive)
        {
            _power01.color = _activeColor;
            _power02.color = _activeColor;
            _power03.color = _activeColor;
        }
        else 
        {
            _power01.color = _deactiveColor;
            _power02.color = _deactiveColor;
            _power03.color = _deactiveColor;
        }
    }

    void FullHP() 
    {
        _health01.enabled = true;
        _health02.enabled = true;
        _health03.enabled = true;
    }
    void MediumHP()
    {
        _health01.enabled = true;
        _health02.enabled = true;
        _health03.enabled = false;
    }
    void LowHp()
    {
        _health01.enabled = true;
        _health02.enabled = false;
        _health03.enabled = false;
    }

    void Laser()
    {
        _power01.enabled = true;
        _power02.enabled = false;
        _power03.enabled = false;
    }

    void Rocket()
    {
        _power01.enabled = false;
        _power02.enabled = true;
        _power03.enabled = false;
    }

    void Triple()
    {
        _power01.enabled = false;
        _power02.enabled = false;
        _power03.enabled = true;
    }

    void Normal()
    {
        _power01.enabled = false;
        _power02.enabled = false;
        _power03.enabled = false;
    }
}
