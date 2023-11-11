using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for looping sprites.
/// It should be attached to the object that needs to loop.
/// It works by moving the sprite to the left and resetting it to its original position when it has moved its entire length.
/// </summary>
public class SpriteLooper : MonoBehaviour
{
    private List<Vector3> originalPositions;
    private List<float> spriteWidths;
    // These sprites should be seamlessly aligned from left to right
    public List<GameObject> spritesToLoop;

[SerializeField] private Spawner spawner;

    private void Start()
    {
        originalPositions = new List<Vector3>();
        spriteWidths = new List<float>();

        foreach (GameObject sprite in spritesToLoop)
        {
            originalPositions.Add(sprite.transform.position);
            spriteWidths.Add(sprite.GetComponent<SpriteRenderer>().bounds.size.x);
        }
    }

    private void Update()
    {
        // Dont loop if the game is not playing
        if (GameManager.Instance.IsPlaying == false) return;

        // Move the sprites
        for (int i = 0; i < spritesToLoop.Count; i++)
        {
            MoveSprite(spritesToLoop[i], originalPositions[i], spriteWidths[i]);
        }
    }

    /// <summary>
    /// This method moves a sprite to the left and resets it to its original position when it has moved its entire length.
    /// </summary>
    /// <param name="sprite">The sprite to move.</param>
    /// <param name="originalPosition">The original position of the sprite.</param>
    /// <param name="spriteWidth">The width of the sprite.</param>
    private void MoveSprite(GameObject sprite, Vector3 originalPosition, float spriteWidth)
    {
        sprite.transform.Translate(Vector3.left * spawner.FactoredObstacleSpeed * Time.deltaTime);

        // Check if the sprite has moved its entire length
        if (sprite.transform.position.x <= originalPosition.x - spriteWidth)
        {
            // Reset the position to the original position
            sprite.transform.position = originalPosition;
        }
    }
}