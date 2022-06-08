using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBehavior : MonoBehaviour
{
    [SerializeField] private HealthBehaviour _playerHealth;
    [SerializeField] private PlayerShootingBehavior _playerShoot;
    [SerializeField] private GameManagerBehavior _gameManager;

    [SerializeField] private Image Health01;
    [SerializeField] private Image Health02;
    [SerializeField] private Image Health03;
    [SerializeField] private Image Power01;
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
}
