using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _startYRotation;
    [SerializeField]
    private float _endYRotation;
    [SerializeField]
    private float _speed = 1;
    private float _lerpValue;

    private void Start()
    {
    }

    private void Update()
    {
        float yRotation = Mathf.LerpAngle(_startYRotation, _endYRotation, Mathf.Sin(Time.time * _speed) * 0.5f + 0.5f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        // transform.rotation = Quaternion.LerpUnclamped(_startYRotation, _startYRotation., Mathf.Sin(Time.time * _speed)
        //transform.position = Vector3.LerpUnclamped(_startPos, _startPos + _offset, Mathf.Sin(Time.time * _speed));

    }
}
