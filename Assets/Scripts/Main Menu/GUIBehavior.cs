using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBehavior : MonoBehaviour
{
    [SerializeField] private HealthBehaviour _playerHealth;
    [SerializeField] private GameManagerBehavior _gameManager;

    [SerializeField] private Image Health01;
    [SerializeField] private Image Health02;
    [SerializeField] private Image Health03;
    [SerializeField] private Image Power01;
    [SerializeField] private Image Power02;
    [SerializeField] private Image Power03;
    [SerializeField] private Text PageText;
    [SerializeField] private Text ScoreText;

    void Start()
    {
        
    }

    void Update()
    {
        ScoreText.text = "Score: " + GameManagerBehavior.Score;
        PageText.text = "Page: " + _gameManager.Page;
        if (_playerHealth.CurrentHealth == 3)
            FullHP();
        if (_playerHealth.CurrentHealth == 2)
            MediumHP();
        if (_playerHealth.CurrentHealth == 1)
            LowHp();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "Normal")
            Normal();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "LaserPowerup")
            Laser();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "RocketPowerup")
            Rocket();
        if (PlayerShootingBehavior.CurrentPowerupHeld == "TripleShotPowerup")
            Triple();
    }

    void FullHP() 
    {
        Health01.enabled = true;
        Health02.enabled = true;
        Health03.enabled = true;
    }
    void MediumHP()
    {
        Health01.enabled = true;
        Health02.enabled = true;
        Health03.enabled = false;
    }
    void LowHp()
    {
        Health01.enabled = true;
        Health02.enabled = false;
        Health03.enabled = false;
    }

    void Laser()
    {
        Power01.enabled = true;
        Power02.enabled = false;
        Power03.enabled = false;
    }

    void Rocket()
    {
        Power01.enabled = false;
        Power02.enabled = true;
        Power03.enabled = false;
    }

    void Triple()
    {
        Power01.enabled = false;
        Power02.enabled = false;
        Power03.enabled = true;
    }

    void Normal()
    {
        Power01.enabled = false;
        Power02.enabled = false;
        Power03.enabled = false;
    }
}
