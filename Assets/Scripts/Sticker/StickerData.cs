using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerData : ScriptableObject
{
    [SerializeField]
    private GameObject _neutralSticker;

    [SerializeField]
    private GameObject _aggressiveSticker;

    public GameObject NeutralSticker 
    {
        get => _neutralSticker; 
        set => _neutralSticker = value;
    }
    public GameObject AggressiveSticker
    { 
        get => _aggressiveSticker;
        set => _aggressiveSticker = value; 
    }
}
