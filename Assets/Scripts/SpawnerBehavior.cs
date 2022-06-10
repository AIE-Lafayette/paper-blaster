using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    private float _rectCornerX = 22.25f;
    private float _rectCornerZ = 12.5f;
    [SerializeField] private GameObject _paperBall;
    [SerializeField] private Transform _playerTransform;

    [SerializeField]
    private List<GameObject> _stickerPrefabs;

    public void SpawnPaper(int amount)
    {
        //Spawn the given amount of objects
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPosition = RandomPointOnPerimeter(0, 0, _rectCornerX, _rectCornerZ);

            GameObject newSpawn = Instantiate(_paperBall, new Vector3(spawnPosition.x, 0.5f, spawnPosition.y), Quaternion.identity);
        }
    }

    public void SpawnSticker(int amount)
    {
        //Spawn the given amount of objects
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPosition = RandomPointOnPerimeter(0, 0, _rectCornerX, _rectCornerZ);

            GameObject sticker = GetRandomSticker();

            GameObject newSpawn = Instantiate(sticker, new Vector3(spawnPosition.x, 0.5f, spawnPosition.y), Quaternion.identity);
            SeekingBehaviour steer = newSpawn.GetComponent<SeekingBehaviour>();
            steer.Target = _playerTransform;
        }
    }

    // Grabs a random sticker prefab from the sticker prefabs list
    public GameObject GetRandomSticker()
    {
        // If the sticker prefabs list is empty, return 
        if (_stickerPrefabs.Count == 0) return null;

        // Returns the sticker prefab at the index of the random number
        int randomNumber = Random.Range(0, _stickerPrefabs.Count);
        return _stickerPrefabs[randomNumber];
    }

    public void SpawnObject(int amount, GameObject spawn)
    {
        //Spawn the given amount of objects
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPosition = RandomPointOnPerimeter(0, 0, _rectCornerX, _rectCornerZ);

            GameObject newSpawn = Instantiate(spawn, new Vector3(spawnPosition.x, 0.5f, spawnPosition.y), Quaternion.identity);
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
