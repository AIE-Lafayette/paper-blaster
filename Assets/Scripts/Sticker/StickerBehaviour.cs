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

    //A reference to the sticker's wandering/seeking behaviours
    private WanderingBehaviour _wanderingBehaviour;
    private SeekingBehaviour _seekingBehaviour;

    //Called when an instance of the sticker is created
    private void Awake()
    {
        _wanderingBehaviour = GetComponent<WanderingBehaviour>();
        _seekingBehaviour = GetComponent<SeekingBehaviour>();
    }

    //Called when the component is added to the scene
    private void Start()
    {
        _currentState = StickerState.Wandering;
    }

    //Called when the sticker is in its wandering state
    private void Wander()
    {
        _seekingBehaviour.enabled = false;
        _wanderingBehaviour.enabled = true;
    }

    //Called when the sticker is in its seeking state
    private void Seek()
    {
        _seekingBehaviour.enabled = true;
        _seekingBehaviour.enabled = false;
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
}
