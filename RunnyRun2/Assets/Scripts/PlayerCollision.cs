using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    /// <summary>
    /// This class is used for detecting collisions between the player and obstacles.
    /// It is attached to the Player game object.
    /// </summary>

    #region Singleton
    public static PlayerCollision Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    [SerializeField] private Transform playerSpriteGroupTransform;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject playerSpriteGroup;
    [SerializeField] private GameObject player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private RingSpawner ringSpawner;
    private Vector3 initPosition;
    public UnityEvent PlayerKilled;


    private void Start()
    {
        initPosition = player.transform.position;
        GameManager.Instance.OnPlay.AddListener(resetPlayer);
    }

    /// <summary>
    /// This method is called when the player collides with an obstacle.
    /// </summary>
    /// <param name="other">The collider of the object the player collided with.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            // disable player collider
            gameObject.GetComponent<Collider2D>().enabled = false;
            // Freezing the player's Y position prevents them from falling through the ground
            FreezePlayerYConstraint(true);
            playerSpriteGroup.SetActive(false);
            PlayerKilled.Invoke();
            SFXManager.Instance.Play("Die");
            // move death particles to player position
            deathParticles.transform.position = player.transform.position;
            deathParticles.Play();
            // waitThenGameOver movement of obstacles and rings
            spawner.PauseObstacles();
            ringSpawner.PauseRings();
            GameManager.Instance.IsPlaying = false;
            // pause for dramatic effect
            StartCoroutine(waitThenGameOver(2f));
        }
    }

    /// <summary>
    /// This method is called when the player collides with a ring.
    /// </summary>
    /// <param name="other">The collider of the object the player collided with.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Ring")
        {
            SFXManager.Instance.Play("CollectRing");
            // Destroy ring and add score
            Destroy(other.gameObject);
            GameManager.Instance.CurrentScore += 1;
        }
    }
    
    /// <summary>
    /// Reset the player's position and re-enable the sprite.
    /// </summary>
    private void resetPlayer()
    {
        playerSpriteGroup.SetActive(true);
        player.transform.position = initPosition;
        FreezePlayerYConstraint(false);
    }

    /// <summary>
    /// This method triggers the Game Over event after a delay.
    /// </summary>
    /// <param name="seconds">The number of seconds to wait before triggering the Game Over event.</param>
    private IEnumerator waitThenGameOver(float seconds)
    {
        // Yield for one second
        yield return new WaitForSeconds(seconds);

        // After the delay, call the Game Over function
        GameManager.Instance.GameOver();
    }

    /// <summary>
    /// This method freezes the player's Y position, or unfreezes it.
    /// </summary>
    /// <param name="freezeY">Whether to freeze the player's Y position.</param>
    public void FreezePlayerYConstraint(bool freezeY)
    {
        if (freezeY)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
