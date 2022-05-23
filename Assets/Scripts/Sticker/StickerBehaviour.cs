using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerBehaviour : MonoBehaviour
{
    //The states the Stickers will use in their state machine
    private enum StickerState
    {
        Wandering,
        Seeking
    }

    //The state that the sticker is currently processing
    private StickerState _currentState;

    //A reference to the sticker's behaviours
    private WanderingBehaviour _wanderingBehaviour;
    private SeekingBehaviour _seekingBehaviour;
    private HealthBehavior _healthBehaviour;
    
    //The seeking range for the sticker. It will only seek if the target is within this range.
    [SerializeField] 
    private float _seekRange;

    //Called when an instance of the sticker is created
    private void Awake()
    {
        _wanderingBehaviour = GetComponent<WanderingBehaviour>();
        _seekingBehaviour = GetComponent<SeekingBehaviour>();
        _healthBehaviour = GetComponent<HealthBehavior>();
    }

    //Called when the component is added to the scene
    private void Start()
    {
        _currentState = StickerState.Wandering;
        _healthBehaviour.CurrentHealth = 3;
    }

    //Called when the sticker is in its wandering state
    private void Wander()
    {
        _seekingBehaviour.enabled = false;
        _wanderingBehaviour.enabled = true;  

        //Something to handle changing stickers' textures to angry   
    }

    //Called when the sticker is in its seeking state
    private void Seek()
    {
        _seekingBehaviour.enabled = true;
        _wanderingBehaviour.enabled = false;
    }

    //Acts on the sticker's current state
    private void ProcessState()
    {
        switch (_currentState)
        {
            case StickerState.Wandering:
            {
                Wander();
                break;
            }
            case StickerState.Seeking:
            {
                Seek();
                break;
            }
        }
    }

    //Changes the sticker's state
    private void UpdateState()
    {
        //If the target is in range and the sticker isn't currently seeking...
        if (_seekingBehaviour.DistanceFromTarget <= _seekRange || _currentState == StickerState.Seeking)
        {
            //Set the state to the seeking state
            _currentState = StickerState.Seeking;
        }
        //Otherwise...
        else 
        {   
            //Set the state to the wandering state
            _currentState = StickerState.Wandering;
        }
    }

    //Called every frame;
    private void Update()
    {
        UpdateState();
        ProcessState();
    }

    //Called on collision with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            GameManagerBehavior.Score++;
            GameManagerBehavior.CurrentScore++;
            other.GetComponent<DeathBehavior>().Death();

            _healthBehaviour.TakeDamage(1);
        }
        else if (other.tag == "Player")
        {
            GameManagerBehavior.Score++;
            GameManagerBehavior.CurrentScore++;
            other.GetComponent<PlayerBehavior>().OnHit();
            _healthBehaviour.TakeDamage(1);
        }
        
        if (!_healthBehaviour.Alive)
        {
            Destroy(gameObject);
        }
    }
}

