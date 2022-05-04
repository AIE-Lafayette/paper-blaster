using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    private int _score;
    private int _difficulty;
    private int _difficultyThreshold;
    private int _difficultyThresholdMax = 5;

    void Start()
    {
        
    }

    void Update()
    {
        if (_difficultyThreshold > _difficultyThresholdMax) 
        {
            _difficultyThreshold = 0;
            _difficultyThresholdMax++;
            _difficulty++;
        }
    }

    public void AddScore(int amount) 
    {
        _score += amount;
        _difficultyThreshold++;
    }
}
