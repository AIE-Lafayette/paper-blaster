using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallBehaviour : MonoBehaviour
{
    Rigidbody _rigidbody;

    [SerializeField]
    //A reference to paper balls
    private GameObject _paperBall;
    
    //The size variations of paper balls
    public enum PaperBallSize { Small, Medium, Large }

    private PaperBallSize _size = PaperBallSize.Large;
    //The size of this paper ball
    private PaperBallSize Size { get => _size; set => _size = value; }

    private Vector3 _moveDirection;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _maxSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //Called when the paper ball is added to a scene
    private void Start()
    {
        ApplyScale();
        ApplyVelocity();
    }

    //Updates the paper ball's scale based on 
    private void ApplyScale()
    {
        switch (Size)
        {
            case PaperBallSize.Small:
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                break;
            }
            case PaperBallSize.Medium:
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            }
            case PaperBallSize.Large:
            {
                transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                break;
            }
            default:
            {
                Debug.Log("This paper ball doesn't have a size!");
                break;
            }
        }
    }

    //Gives this paper ball a velocity
    private void ApplyVelocity()
    {
        //Gives the ball a random direction
        _moveDirection = new Vector3(Random.Range(-500.0f, 500.0f), 0, Random.Range(-500.0f, 500.0f)).normalized;

        //Move in the correct direction scaled up by the move speed
        _rigidbody.AddForce(_moveDirection * _moveSpeed * Time.deltaTime, ForceMode.Impulse);


        //If the object is moving above the max speed
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            //Set the velocity to be the max speed
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
    }

    //Called upon collision with other objects
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            //If the paper ball collides with a player or bullet...
            case "Player": 
            {
                Break();
                //Award the player with points
                Destroy(gameObject);
                break;
            }
            case "PlayerBullet":
            {
                if (!Break())
                {
                    //Award the player with points here
                }
                Destroy(gameObject);
                break;
            }
        }
    }

    private bool Break() 
    {
        //If the paper ball can't be broken, return
        if (Size == PaperBallSize.Small) return false;

        //Creates two smaller paper balls
        for (int i = 0; i < 2; i++)
        {
            GameObject paperBall = Instantiate(_paperBall);
            PaperBallBehaviour paperBallBehaviour = paperBall.GetComponent<PaperBallBehaviour>();
            paperBallBehaviour.Initiate(transform.position, Size - 1, _rigidbody.velocity, _moveSpeed + 10);
        }

        return true;
    }

    public void Initiate(Vector3 position, PaperBallSize size, Vector3 velocity, float moveSpeed)
    {
        transform.position = position;
        Size = size;
        _moveSpeed = moveSpeed;
        _rigidbody.velocity = velocity;
    }

    void Update() 
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Break();
        //    Destroy(gameObject);
        //}
    }
}
