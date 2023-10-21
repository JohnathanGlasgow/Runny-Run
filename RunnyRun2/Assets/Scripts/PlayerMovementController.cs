using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    // player input component
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpTime = 0.3f;

    private bool isGrounded = false;      // Flag to check if the player is grounded.
    private bool isJumping = false;       // Flag to check if the player is in a jump state.
    private float jumpTimer;              // Timer to control the maximum jump time.
    private GameManager gameManager;
    private InputAction jumpAction;

    // listen for onplay event from game manager
    // when onplay event is called, set the animator bool "isRunning" to true
    // when ongameover event is called, set the animator bool "isRunning" to false
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlay.AddListener(onGameStart);
        gameManager.OnGameOver.AddListener(onGameOver);
    }

    private void Update()
    {
        // see if there overlap between player and ground layer
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // the player is on or near the ground and the jumptimer has exceeded the jump time limit
        if ((isGrounded == true) && (jumpTimer >= jumpTime))
        {
            // stop the jump animation
            animator.SetBool("IsJumping", false);
        }
        else if (jumpTimer < jumpTime)
        {
            jumpTimer += Time.deltaTime; // Increment the jump timer.
            if (isJumping == true)
            {
                // The player is still holding the jump button, and the jump time hasn't exceeded the limit.
                rb.velocity = Vector2.up * jumpForce; // Apply additional jump force.
            }
        }
        else
        {
            // Jump time limit has been reached, so the player should stop jumping.
            isJumping = false;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (isGrounded)
                {
                    jumpTimer = 0;
                    isJumping = true;
                    animator.SetBool("IsJumping", true);
                    rb.velocity = Vector2.up * jumpForce;
                }
                break;
            case InputActionPhase.Canceled:
                isJumping = false;
                break;
        }
    }

    private void onGameStart()
    {
        // enable player input
        playerInput.enabled = true;
        animator.SetBool("IsRunning", true);
    }

    private void onGameOver()
    {
        // disable player input
        playerInput.enabled = false;
        animator.SetBool("IsRunning", false);
    }
}


