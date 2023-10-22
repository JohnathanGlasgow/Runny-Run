using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	/// <summary>
	/// This class is used for detecting collisions between the player and obstacles.
	/// It is attached to the Player game object.
	/// </summary>
	
    [SerializeField] private Transform playerSpriteGroup;
	private Vector3 initPosition;
	private void Start() {
		initPosition = transform.position;
		GameManager.Instance.OnPlay.AddListener(activatePlayer);
	}

	private void activatePlayer() {
		resetPlayer();
		gameObject.SetActive(true);
		
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Obstacle") {
			gameObject.SetActive(false);
            GameManager.Instance.GameOver();
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.tag == "Ring") {
			// destroy ring and add score
			Destroy(other.gameObject);
			GameManager.Instance.CurrentScore += 1;
		}
	}
    private void resetPlayer()
    {
        playerSpriteGroup.localRotation = Quaternion.identity;
		transform.position = initPosition;
    }
}
