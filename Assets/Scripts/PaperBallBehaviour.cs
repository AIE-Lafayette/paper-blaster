using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallBehaviour : MonoBehaviour
{
    [SerializeField]
    //A reference to paper balls
    private GameObject _paperBall;

    //The time that the paper ball was spawned
    private float _spawnTime;

    //The cooldown for breaking into more paper balls
    private float _breakCooldown;

    
    //The size variations of paper balls
    public enum PaperBallSize { Small, Medium, Large }

    private PaperBallSize _size = PaperBallSize.Large;
    //The size of this paper ball
    private PaperBallSize Size { get => _size; set => _size = value; }



    //Called when the paper ball is added to a scene
    private void Start()
    {
        UpdateScale();
    }

    //Updates the paper ball's scale based on 
    private void UpdateScale()
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
                transform.localScale = new Vector3(1, 1, 1);
                break;
            }
            default:
            {
                Debug.Log("This paper ball doesn't have a size!");
                break;
            }
        }
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
                break;
            }
            case "PlayerBullet":
            {
                if (!Break())
                {
                    //Award the player with points here
                }
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

            paperBallBehaviour.Size = Size - 1;

            PaperBallMovementBehaviour movementBehaviour = GetComponent<PaperBallMovementBehaviour>();
            PaperBallMovementBehaviour newMovementBehaviour = paperBall.GetComponent<PaperBallMovementBehaviour>();
            newMovementBehaviour.PreviousDirection = movementBehaviour.MoveDirection;
            newMovementBehaviour.MoveSpeed *= 5;
            newMovementBehaviour.MaxSpeed *= 5;
>>>>>>> parent of f521609 (Remaking paperballbehaviour)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Break();
            Destroy(gameObject);
        }
    }
}
