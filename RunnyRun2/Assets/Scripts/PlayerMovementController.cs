using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is used for controlling the player's movement.
/// It is attached to the Player game object.
/// Inputs are handled using the new Input System.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpTime = 0.3f;

    // Flag to check if the player is grounded.
    private bool isGrounded = false;
    // Flag to check if the player is in a jump state.    
    private bool isJumping = false;
    // Timer to control the maximum jump time.       
    private float jumpTimer;
    // Flag to check if the player has completed a jump.
    private bool jumpComplete;
    private GameManager gameManager;
    private InputAction jumpAction;


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
            jumpComplete = true;
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

    /// <summary>
    /// This method is callled when the input system detects input.
    public void OnInteraction(InputAction.CallbackContext context)
    {
        // If the game is not playing, do nothing.
        if (GameManager.Instance.IsPlaying == false) return;
        switch (context.phase)
        {
            // If the jump button is pressed, and the player is on the ground, start the jump.
            case InputActionPhase.Started:
                if (isGrounded && jumpComplete)
                {
                    SFXManager.Instance.Play("Jump");
                    jumpComplete = false;
                    jumpTimer = 0;
                    isJumping = true;
                    animator.SetBool("IsJumping", true);
                    rb.velocity = Vector2.up * jumpForce;
                }
                break;
            // If the jump button is released, stop the jump.
            case InputActionPhase.Canceled:
                isJumping = false;
                break;
        }
    }

    /// <summary>
    /// Handle the game start event.
    /// </summary>
    private void onGameStart()
    {
        playerInput.enabled = true;
    }

    /// <summary>
    /// Handle the game over event.
    /// </summary>
    private void onGameOver()
    {
        playerInput.enabled = false;
        reset();
        animator.SetBool("IsRunning", false);
    }

    /// <summary>
    /// This method resets the fields that govern the player's jump.
    /// </summary>
    private void reset()
    {
        // reset all the fields to their initial values
        jumpTimer = 0;
        isJumping = false;
        isGrounded = false;
        jumpComplete = false;
    }
}


