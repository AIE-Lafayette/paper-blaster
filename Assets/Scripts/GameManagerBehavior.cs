using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    //Score and difficulty
    private int _score;
    private int _difficulty;
    private int _difficultyThreshold;
    private int _difficultyThresholdMax = 5;

    //Spawning Asteroids variables
    [SerializeField] private GameObject _paperball;
    //[SerializeField] private List<GameObject> _asteroids;
    public static int PaperBalls;
    private float _rectCornerX = 22.25f;
    private float _rectCornerZ = 12.5f;

    void Start()
    {
        //For testing only
        SpawnAsteroids(5);
    }

    void Update()
    {
        Debug.Log(PaperBalls);

        //If the difficulty threshold is more than the max difficulty threshold
        if (_difficultyThreshold > _difficultyThresholdMax) 
        {
            //Increase difficulty
            _difficultyThreshold = 0;
            _difficultyThresholdMax++;
            _difficulty++;
        }
        //Check for asteroids
        AsteroidCheck();
    }

    public void AddScore(int amount) 
    {
        _score += amount;
        _difficultyThreshold++;
    }

    void SpawnAsteroids(int amount) 
    {
        //Spawn the given amount of asteroids
        for (int i = 0; i < amount; i++) 
        {
            //Spawn an asteroid in a random position and add it to the list of asteroids
            Vector2 pos = RandomPointOnPerimeter(0, 0, _rectCornerX, _rectCornerZ);
            GameObject spawn = Instantiate(_paperball, new Vector3(pos.x, 0.5f, pos.y), Quaternion.identity);
            PaperBalls++;
            //spawn.GetComponent<PaperBallBehaviour>().GameManager = this;
        }
    }

    void AsteroidCheck() 
    {
        ////Check if destroyed asteroids to remove them from the list
        //for (int i = 0; i < _asteroids.Count; i++) 
        //{
        //    //if(_asteroids[i] == null)
        //        /_asteroids.RemoveAt(i);
        //}
        //If there are no more asteroids
        if (PaperBalls == 0)
        {
            Debug.Log("All Paper balls cleared.");
        }
    }

    Vector2 RandomPointOnPerimeter(float x1, float y1, float x2, float y2) 
    {
        //Pick a random side of the rectangle
        Vector2 point = new Vector2();
        int side = Random.Range(0, 4);
        //Bottom side
        if (side == 0)
        {
            point.x = Random.Range(0, x2);
            point.y = y1;
        }
        //Top side
        if (side == 1)
        {
            point.x = Random.Range(0, x2);
            point.y = y2;
        }
        //Left side
        if (side == 2)
        {
            point.x = x1;
            point.y = Random.Range(0, y2);
        }
        //Right side
        if (side == 3)
        {
            point.x = x2;
            point.y = Random.Range(0, y2);
        }
        return point;
    }
}
