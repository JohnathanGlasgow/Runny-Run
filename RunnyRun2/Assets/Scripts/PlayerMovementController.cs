using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    private bool isGrounded = false;      // Flag to check if the player is grounded.
    private bool isJumping = false;       // Flag to check if the player is in a jump state.
    private float jumpTimer;              // Timer to control the maximum jump time.


    private InputAction jumpAction;


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        if (isJumping)
        {
            if (jumpTimer < jumpTime)
            {
                // The player is still holding the jump button, and the jump time hasn't exceeded the limit.
                rb.velocity = Vector2.up * jumpForce; // Apply additional jump force.
                jumpTimer += Time.deltaTime; // Increment the jump timer.
            }
            else
            {
                // Jump time limit has been reached, so the player should stop jumping.
                isJumping = false;
            }
        }

    }


    public void OnInteraction(InputAction.CallbackContext context)
    {
        // log this
        Debug.Log("OnInteraction called");

        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("Interaction started");
                if (isGrounded)
                {
                    isJumping = true;
                    rb.velocity = Vector2.up * jumpForce; // Apply an initial jump force.
                }
                break;
            case InputActionPhase.Canceled:
                Debug.Log("Interaction canceled");
                // log this
                Debug.Log("Jumping false");
                isJumping = false;
                jumpTimer = 0;
                break;
        }
    }

}


