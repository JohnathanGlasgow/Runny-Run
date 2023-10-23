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

    private void activatePlayer()
    {
        
        //gameObject.SetActive(true);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            playerSpriteGroup.SetActive(false);
			PlayerKilled.Invoke();
			// move death particles to player position
			deathParticles.transform.position = player.transform.position;
			deathParticles.Play();
			spawner.PauseObstacles();
			ringSpawner.PauseRings();
			GameManager.Instance.IsPlaying = false;
            StartCoroutine(Pause(2f));
			//gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Ring")
        {
            // destroy ring and add score
            Destroy(other.gameObject);
            GameManager.Instance.CurrentScore += 1;
        }
    }
    private void resetPlayer()
    {
		// log out
		Debug.Log("resetPlayer");
		playerSpriteGroup.SetActive(true);
        //playerSpriteGroupTransform.localRotation = Quaternion.identity;
        player.transform.position = initPosition;
		// set rotation to 0

    }

    private IEnumerator Pause(float seconds)
    {
        // Yield for one second
        yield return new WaitForSeconds(seconds);

        // After the delay, call the Game Over function
        GameManager.Instance.GameOver();
    }

	public void FreezePlayerYConstraintPlayer(bool freezeY)
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
