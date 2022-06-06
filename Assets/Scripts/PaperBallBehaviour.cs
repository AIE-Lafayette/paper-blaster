using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallBehaviour : MonoBehaviour
{
    [SerializeField]
    //A reference to paper balls
    private GameObject _paperBall;

    //The size variations of paper balls
    public enum PaperBallSize { Small, Medium, Large }

    private PaperBallSize _size = PaperBallSize.Large;
    //The size of this paper ball
    private PaperBallSize Size { get => _size; set => _size = value; }

    //The owner's health behaviour
    private HealthBehaviour _healthBehaviour;

    private void Awake()
    {
        _healthBehaviour = GetComponent<HealthBehaviour>();
    }

    //Called when the paper ball is added to a scene
    private void Start()
    {
        GameManagerBehavior.CurrentPaperAmount++;
        //Updates the paper ball's size
        UpdateScale();

        //Gives the paper ball a random direction
        MovementBehavior movementBehavior = GetComponent<MovementBehavior>();
        movementBehavior.MoveDirection = new Vector3(Random.Range(-500.0f, 500.0f), 0, Random.Range(-500.0f, 500.0f)).normalized;
        movementBehavior.Move();

        _healthBehaviour.CurrentHealth = 1;
        _healthBehaviour.OnDeath = Break;
    }

    //Updates the paper ball's scale based on 
    private void UpdateScale()
    {
        switch (Size)
        {
            case PaperBallSize.Small:
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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
                       //If the paper ball collides with a player, break the ball and damage the player.
            case "Player": 
            {
                other.GetComponent<PlayerBehavior>().OnHit();
                _healthBehaviour.TakeDamage(1);
                break;
            }
        }
    }

    private void Break(object value) 
    {
        GameManagerBehavior.CurrentPaperAmount--;

        //If the paper ball can't be broken, return
        if (Size == PaperBallSize.Small)
        {
            GameManagerBehavior.IncreaseScore(1);
            Destroy(gameObject);
            return;
        }

        //Creates two smaller paper balls
        for (int i = 0; i < 2; i++)
        {
            MovementBehavior movementBehavior = GetComponent<MovementBehavior>();
            GameObject paperBall = Instantiate(_paperBall);

            PaperBallBehaviour newPBBehaviour = paperBall.GetComponent<PaperBallBehaviour>();
            newPBBehaviour.Initiate(transform.position, Size - 1,
            movementBehavior.Rigidbody.velocity, movementBehavior.MoveSpeed);
        }

        Destroy(gameObject);
    }

    public void Initiate(Vector3 position, PaperBallSize size, Vector3 velocity, float moveSpeed)
    {
        MovementBehavior movementBehavior = GetComponent<MovementBehavior>();
        transform.position = new Vector3(position.x, 0.5f, position.z);
        Size = size;
        movementBehavior.MoveSpeed = moveSpeed;
        movementBehavior.Rigidbody.velocity = velocity;
    }
}
