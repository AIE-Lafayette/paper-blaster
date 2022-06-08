using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStickerBehaviour : MonoBehaviour
{   
    // A reference to the sticker's health behaviour
    [SerializeField]
    private HealthBehaviour _healthBehaviour;

    // A reference to the sticker's sprites
    [SerializeField]
    private GameObject _neutralSticker;
    [SerializeField]
    private GameObject _aggressiveSticker;

    [SerializeField]
    private Material _holographicSticker;

    // The chance of each sticker to hold a power-up
    [SerializeField]
    private float _powerUpChance;

    // A reference to each of the power up prefabs
    [SerializeField]
    private GameObject _tripleShotPowerUp;

    [SerializeField]
    private GameObject _rocketPowerUp;

    [SerializeField]
    private GameObject _laserPowerUp;

    // Called when the sticker is instantiated
    private void Awake()
    {
        // The stickers have a ten percent chance of spawning 
        int randomNumber = Random.Range(0, 101);
        if (randomNumber > _powerUpChance) return;

        // Changes the sticker's material if it holds a power-up
        _neutralSticker.GetComponent<SpriteRenderer>().material = _holographicSticker;
        _aggressiveSticker.GetComponent<SpriteRenderer>().material = _holographicSticker;

        // The sticker whill drop a random power-up on death
        _healthBehaviour.OnDeath += (gameObject) => 
        {
            DropRandomPowerUp();
        };
    }

    //Gives the sticker a random powerup to drop on death;
    private void DropRandomPowerUp()
    {

    }
}
