using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;


/// <summary>
/// This helper class contains an obstacle prefab and its difficulty tier.
/// </summary>
[Serializable]
public class ObstacleInfo
{
    public GameObject ObstaclePrefab;
    public int DifficultyTier = 1;
}

/// <summary>
/// This class is used for spawning obstacles.
/// It is attached to the Spawner game object.
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private ObstacleInfo[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;

    [SerializeField] private float obstacleSpawnTime = 3f;
    [SerializeField][Range(0, 1)] private float obstacleSpawnTimeFactor = 0.1f;
    private float factoredObstacleSpawnTime;

    [SerializeField] private float obstacleSpeed = 4f;
    [SerializeField][Range(0, 1)] private float obstacleSpeedFactor = 0.2f;
    public float FactoredObstacleSpeed { get; set; }
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private void Start()
    {
        GameManager.Instance.OnPlay.AddListener(clearObstacles);
        GameManager.Instance.OnPlay.AddListener(resetFactors);

        timeUntilObstacleSpawn = obstacleSpawnTime;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            timeAlive += Time.deltaTime;

            calculateFactors();

            spawnLoop();
        }
    }

    /// <summary>
    /// This method is used for spawning obstacles.
    /// It checks if the time until the next obstacle spawn has been reached.
    /// If it has, it spawns an obstacle and resets the time until the next spawn.
    /// </summary>
    private void spawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= factoredObstacleSpawnTime)
        {
            spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    /// <summary>
    /// This method is used for pausing the obstacles.
    /// </summary>
    public void PauseObstacles()
    {
        FactoredObstacleSpeed = 0f;
        foreach (Transform child in obstacleParent)
        {
            child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// This method is used for destroying all bstacles.
    /// </summary>
    private void clearObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// This method is used for calculating the factored obstacle spawn time and speed.
    /// </summary>
    private void calculateFactors()
    {
        factoredObstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        FactoredObstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    /// <summary>
    /// This method is used for resetting the time alive and factored obstacle spawn time and speed.
    /// </summary>
    private void resetFactors()
    {
        timeAlive = 1f;
        factoredObstacleSpawnTime = obstacleSpawnTime;
        FactoredObstacleSpeed = obstacleSpeed;

    }

    private void spawn()
    {
        if (FactoredObstacleSpeed < 5f) return;

        // Get obstacleToSpawn candidates based on difficulty tier
        int difficultyTier = GameManager.Instance.DifficultyTier;
        List<ObstacleInfo> possibleObstacles = new List<ObstacleInfo>();
        foreach (ObstacleInfo obstacleInfo in obstaclePrefabs)
        {
            if (obstacleInfo.DifficultyTier <= difficultyTier)
            {
                possibleObstacles.Add(obstacleInfo);
            }
        }
        // Randomly select an obstacle from the candidates and spawn it
        ObstacleInfo obstacleToSpawn = possibleObstacles[UnityEngine.Random.Range(0, possibleObstacles.Count)];
        GameObject obstacle = Instantiate(obstacleToSpawn.ObstaclePrefab, transform.position, Quaternion.identity);
        obstacle.transform.parent = obstacleParent;

        // Find its Rigidbody2D and set its velocity to the factored obstacle speed
        Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * FactoredObstacleSpeed;
    }
}

