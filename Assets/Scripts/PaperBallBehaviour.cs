using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallBehaviour : MonoBehaviour
{
    private GameObject _paperBall;
    
    //The size variations of paper balls
    private enum PaperBallSize { Small, Medium, Large }

    //The size of this paper ball
    private PaperBallSize _size;

    //Called when the paper ball is added to a scene
    private void Start()
    {
        _size = PaperBallSize.Large;
        UpdateScale();
    }

    //Updates the paper ball's scale based on 
    private void UpdateScale()
    {
        switch (_size)
        {
            case PaperBallSize.Small:
            {
                transform.localScale = new Vector3(1, 1, 1);
                break;
            }
            case PaperBallSize.Medium:
            {
                transform.localScale = new Vector3(3, 3, 3);
                break;
            }
            case PaperBallSize.Large:
            {
                transform.localScale = new Vector3(5, 5, 5);
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
                break;
            }
        }
    }

    private void Break() 
    {
        
    }
}
