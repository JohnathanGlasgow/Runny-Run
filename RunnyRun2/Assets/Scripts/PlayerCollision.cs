using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private void Start() {
		GameManager.Instance.onPlay.AddListener(activatePlayer);
	}

	private void activatePlayer() {
		gameObject.SetActive(true);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Obstacle") {
			gameObject.SetActive(false);
            GameManager.Instance.GameOver();
		}
	}

}
