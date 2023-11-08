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
        GameManager.Instance.OnPlay.AddListener(ClearObstacles);
        GameManager.Instance.OnPlay.AddListener(resetFactors);

        timeUntilObstacleSpawn = obstacleSpawnTime;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            timeAlive += Time.deltaTime;

            CalculateFactors();

            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= factoredObstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    public void PauseObstacles()
    {
        FactoredObstacleSpeed = 0f;
        foreach (Transform child in obstacleParent)
        {
            child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void ClearObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors()
    {
        factoredObstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        FactoredObstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);

    }

    private void resetFactors()
    {
        timeAlive = 1f;
        factoredObstacleSpawnTime = obstacleSpawnTime;
        FactoredObstacleSpeed = obstacleSpeed;

    }

    private void Spawn()
    {
        if (FactoredObstacleSpeed < 5f) return;

        // GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        // get obstacleToSpawn based on difficulty tier
        int difficultyTier = GameManager.Instance.DifficultyTier;
        List<ObstacleInfo> possibleObstacles = new List<ObstacleInfo>();
        foreach (ObstacleInfo obstacleInfo in obstaclePrefabs)
        {
            if (obstacleInfo.DifficultyTier <= difficultyTier)
            {
                possibleObstacles.Add(obstacleInfo);
            }
        }
        ObstacleInfo obstacleToSpawn = possibleObstacles[UnityEngine.Random.Range(0, possibleObstacles.Count)];
        GameObject obstacle = Instantiate(obstacleToSpawn.ObstaclePrefab, transform.position, Quaternion.identity);
        obstacle.transform.parent = obstacleParent;

        Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();

        obstacleRB.velocity = Vector2.left * FactoredObstacleSpeed;
    }
}

