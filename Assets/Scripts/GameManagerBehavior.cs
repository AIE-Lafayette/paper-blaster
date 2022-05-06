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
    [SerializeField] private GameObject _asteroid;
    private List<GameObject> _asteroids;
    private float _rectX = 22.25f;
    private float _rectZ = 12.5f;

    void Start()
    {
        SpawnAsteroids(5);
    }

    void Update()
    {
        if (_difficultyThreshold > _difficultyThresholdMax) 
        {
            _difficultyThreshold = 0;
            _difficultyThresholdMax++;
            _difficulty++;
        }
        AsteroidCheck();
    }

    public void AddScore(int amount) 
    {
        _score += amount;
        _difficultyThreshold++;
    }

    void SpawnAsteroids(int amount) 
    {
        for (int i = 0; i < amount; i++) 
        {
            Vector2 pos = new Vector2();
            int side = Random.Range(0, 4);
            if (side == 0) 
            {
                pos.x = Random.Range(0, _rectX);
                pos.y = 0;
            }
            if (side == 1)
            {
                pos.x = Random.Range(0, _rectX);
                pos.y = _rectZ;
            }
            if (side == 2)
            {
                pos.x = 0;
                pos.y = Random.Range(0, _rectZ);
            }
            if (side == 3)
            {
                pos.x = _rectX;
                pos.y = Random.Range(0, _rectZ);
            }
            GameObject spawn = Instantiate(_asteroid, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
            _asteroids.Add(spawn);
        }
        Debug.Log("Spawned Asteroids");
    }

    void AsteroidCheck() 
    {
        for (int i = 0; i < _asteroids.Count; i++) 
        {
            if(_asteroids[i] == null)
                _asteroids.RemoveAt(i);
        }
        if (_asteroids.Count == 0)
        {
            Debug.Log("All asteroids cleared.");
        }
    }
}
