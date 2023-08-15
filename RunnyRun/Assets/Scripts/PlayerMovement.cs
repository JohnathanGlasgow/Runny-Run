using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform GFX;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Transform feetPos;
	[SerializeField] private float groundDistance = 0.25f;
	[SerializeField] private float jumpTime = 0.3f;

	[SerializeField] private float crouchHeight = 0.5f;

	private bool isGrounded = false;
	private bool isJumping = false;
	private float jumpTimer;

    private void Update()
    {
		isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

		#region JUMPING
		if (isGrounded && Input.GetButtonDown("Jump")) {
			isJumping = true;
			rb.velocity = Vector2.up * jumpForce;
		}

		if (isJumping && Input.GetButton("Jump")) {

			if (jumpTimer < jumpTime) {
				rb.velocity = Vector2.up * jumpForce;

				jumpTimer += Time.deltaTime;
			} else {
				isJumping = false;
			}

		}

		if (Input.GetButtonUp("Jump")) {
			isJumping = false;
			jumpTimer = 0;
		}
		#endregion
		#region CROUCHING
		
		if (isGrounded && Input.GetButton("Crouch")) {
			GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);

			if (isJumping) {
				GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
			}
		}

		if (Input.GetButtonUp("Crouch")) {
			GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
		}

		#endregion
    }
}
