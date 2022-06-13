using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleBehaviour : MonoBehaviour
{
    // How far the object will waddle
    [SerializeField]
    private float _startYRotation;
    [SerializeField]
    private float _endYRotation;

    [SerializeField]
    private float _speed = 1;

    //The speed of the waddling effect
    public float Speed
    {
        get => _speed;  
        set => _speed = value;
    }

    private void Update()
    {
        // Uses LERP to rotate the sticker back and forth, mimicing waddling
        float yRotation = Mathf.LerpAngle(_startYRotation, _endYRotation, Mathf.Sin(Time.time * _speed) * 0.5f + 0.5f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
