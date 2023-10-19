using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	/// <summary>
	/// This class is used for detecting collisions between the player and obstacles.
	/// It is attached to the Player game object.
	/// </summary>
	
	private void Start() {
		GameManager.Instance.OnPlay.AddListener(activatePlayer);
	}

	private void activatePlayer() {
		gameObject.SetActive(true);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Obstacle") {
			// log the collision
			Debug.Log("Player collided with obstacle");
			gameObject.SetActive(false);
            GameManager.Instance.GameOver();
		}
	}

}
