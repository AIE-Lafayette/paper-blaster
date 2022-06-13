using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStickerBehaviour : MonoBehaviour
{   
    // Contains the power up prefabs
    [SerializeField]
    private List<GameObject> _powerUps;

    // The chance for stickers to become power-ups;
    [SerializeField]
    private float _powerUpChance = 10.0f;

    // A reference to the owner's sticker behaviour
    [SerializeField]
    private StickerBehaviour _stickerBehaviour;

    // A reference to the sticker's sprites
    [SerializeField]
    private SpriteRenderer _neutralSticker;
    [SerializeField]
    private SpriteRenderer _aggressiveSticker;

    [SerializeField]
    private Material _holographicSticker;

    // Called when the sticker is added to the scene
    private void Start()
    {
        // The stickers have a ten percent chance of spawning 
        int randomNumber = Random.Range(0, 101);
        if (randomNumber > _powerUpChance) return;

        // Changes the sticker's material
        _neutralSticker.material = _holographicSticker;
        _aggressiveSticker.material = _holographicSticker;  

        _stickerBehaviour.AggressiveMaterial = _aggressiveSticker.material;

        // The sticker whill drop a random power-up on death
        _stickerBehaviour.AfterDissolve += (gameObject) =>
        {
            DropRandomPowerUp();
        };
    }

    // Gives the sticker a random powerup to drop on death;
    private void DropRandomPowerUp()
    {
        Debug.Log("DropRandomPowerUp");
        // Gets a random number from 0 to 2;, and uses that number as an index to get a power-up
        int randomNumber = Random.Range(0, _powerUps.Count);
        GameObject powerUp = _powerUps[randomNumber];

        // If the power-up is null, give an error message
        if (powerUp == null)
        {
            Debug.Log("A power-up sticker is missing its power-up!");
            return;
        }

        // Otherwise, create an instance of the powerup at the sticker's position
        Instantiate(powerUp, transform.position, Quaternion.identity);
    }
}
