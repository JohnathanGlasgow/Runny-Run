using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for spawning rings.
/// </summary>

public class RingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float ringSpeed = 4f;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform ringParent;
    private float spawnTimer;

    void Start()
    {
        GameManager.Instance.OnPlay.AddListener(clearRings);
    }

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            // If the spawn timer has exceeded the spawn rate
            if (spawnTimer >= spawnRate)
            {
                // Spawn a ring
                spawnRing();
                // Reset the spawn timer
                spawnTimer = 0f;
            }
            else
            {
                // Increment the spawn timer
                spawnTimer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// This method spawns a ring at a random spawn point.
    /// </summary>
    private void spawnRing()
    {
        // Get a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Instantiate a ring at the spawn point
        GameObject newRing = Instantiate(ringPrefab, spawnPoint.position, Quaternion.identity);

        // Set the parent of the instantiated object to the ringParent
        newRing.transform.SetParent(ringParent);
        
        // Get the Rigidbody2D component from the instantiated object
        Rigidbody2D rB = newRing.GetComponent<Rigidbody2D>();

        // Set the velocity to move left
        rB.velocity = Vector2.left * ringSpeed;
    }

    /// <summary>
    /// This method clears all rings from the scene.
    /// </summary>
    private void clearRings()
    {
        foreach(Transform child in ringParent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// This method pauses all rings.
    /// </summary>
    public void PauseRings()
    {
        foreach(Transform child in ringParent)
        {
            child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
