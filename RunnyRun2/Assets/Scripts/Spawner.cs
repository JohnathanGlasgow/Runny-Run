using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for spawning obstacles.
/// It is attached to the Spawner game object.
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;

    [SerializeField] private float obstacleSpawnTime = 3f;
    [SerializeField][Range(0, 1)] private float obstacleSpawnTimeFactor = 0.1f;
    private float factoredObstacleSpawnTime;

    [SerializeField] private float obstacleSpeed = 4f;
    [SerializeField][Range(0, 1)] private float obstacleSpeedFactor = 0.2f;
    public float FactoredObstacleSpeed { get; private set;}

    
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private void Start()
    {
        GameManager.Instance.OnGameOver.AddListener(ClearObstacles);
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

    private void ClearObstacles()
    {
        foreach(Transform child in obstacleParent)
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

        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject obstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        obstacle.transform.parent = obstacleParent;

        Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();

        obstacleRB.velocity = Vector2.left * FactoredObstacleSpeed;
    }
}

