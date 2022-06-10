using UnityEngine;

public class StickerBehaviour : MonoBehaviour
{
    //The states the Stickers will use in their state machine
    private enum StickerState
    {
        Neutral,
        Aggressive,
        Dead
    }

    //The state that the sticker is currently processing
    private StickerState _currentState;

    // Called after the sticker's dissolve animations ends
    private DeathEventHandler _afterDissolve;

    //A reference to the sticker's behaviours
    [SerializeField]
    private WanderingBehaviour _wanderingBehaviour;
    [SerializeField]
    private SeekingBehaviour _seekingBehaviour;
    [SerializeField]
    private HealthBehaviour _healthBehaviour;   
    [SerializeField]
    private WaddleBehaviour _waddleBehaviour;
    [SerializeField]
    private StickerMovementBehaviour _stickerMovementBehaviour;

    //References to the textures of this sticker
    [SerializeField]
    private GameObject _neutralSticker;
    [SerializeField]
    private GameObject _aggressiveSticker;
    
    //The seeking range for the sticker. It will only seek if the target is within this range.
    [SerializeField] 
    private float _seekRange;

    public DeathEventHandler AfterDissolve 
    {
        get => _afterDissolve;
        set => _afterDissolve = value;
     }

    // The chance of the stickers to hold power-ups

    //Called when an instance of this component is created
    private void Awake()
    {
        //Sets the sticker's aggressive texture as inactive
        _aggressiveSticker.SetActive(false);
        _neutralSticker.SetActive(true);
    }

    //Called when the component is added to the scene
    private void Start()
    {
        //Increases the current sticker counter
        GameManagerBehavior.CurrentStickerAmount++;

        //Sets the sticker's current state
        _currentState = StickerState.Neutral;
        _healthBehaviour.CurrentHealth = 3;
        _healthBehaviour.OnDeath = (gameObject) => 
        {
            Dissolve();
        };

        //Assigns what happens after the sticker disolves
        _afterDissolve = Destroy;
        _afterDissolve += ( gameObject ) => 
        {
            GameManagerBehavior.CurrentStickerAmount--;
            GameManagerBehavior.IncreaseScore(2);
        };
    }

    //Called when the sticker is in its wandering state
    private void Wander()
    {
        _seekingBehaviour.enabled = false;
        _wanderingBehaviour.enabled = true;  

        _waddleBehaviour.Speed = 4;
        _stickerMovementBehaviour.MaxSpeed = 1.0f;
    }

    //Called when the sticker is in its seeking state
    private void Seek()
    {
        _seekingBehaviour.enabled = true;
        _wanderingBehaviour.enabled = false;

        _waddleBehaviour.Speed = 12;
        _stickerMovementBehaviour.MaxSpeed = 1.5f;
    }

    private void Dissolve()
    {

    }

    //Acts on the sticker's current state
    private void ProcessState()
    {
        switch (_currentState)
        {
            case StickerState.Neutral:
            {
                Wander();
                break;
            }
            case StickerState.Aggressive:
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
        if (_seekingBehaviour.DistanceFromTarget <= _seekRange || _currentState == StickerState.Aggressive)
        {
            //Set the state to the seeking state
            _currentState = StickerState.Aggressive;
            //Change the sticker's texture
            _neutralSticker.SetActive(false);
            _aggressiveSticker.SetActive(true);
        }
        //Otherwise...
        else 
        {   
            //Set the state to the wandering state
            _currentState = StickerState.Neutral;
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
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerBehavior>().OnHit();
            _healthBehaviour.TakeDamage(1);
        }
    }
}

