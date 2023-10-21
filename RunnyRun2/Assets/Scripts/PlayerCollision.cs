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
			// log the collision
			gameObject.SetActive(false);
            GameManager.Instance.GameOver();
		}
	}
    private void resetPlayer()
    {
        // log out  the call
        Debug.Log("resetPlayer called");
        // set playerSpriteGroup.transform.rotation 0, 0, 0
		// logout the current rotation
		Debug.Log("current rotation: " + playerSpriteGroup.transform.localRotation);
        playerSpriteGroup.localRotation = Quaternion.identity;
		// logout the new rotation
		Debug.Log("new rotation: " + playerSpriteGroup.transform.localRotation);

		// set this.transform.position to initPosition
		transform.position = initPosition;
    }
}
