using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesBehavior : MonoBehaviour
{
    private int _lives;
    private Rigidbody _player;

    private void Start()
    {
        _player = GetComponent<Rigidbody>();
    }

    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PaperBall")
        {
            _lives--;
            _player.position = new Vector3(0f, .5f, 0f);
            _player.velocity = Vector3.zero;
        }
            
    }


}
