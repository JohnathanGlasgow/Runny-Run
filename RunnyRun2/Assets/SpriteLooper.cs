using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLooper : MonoBehaviour
{
    private List<Vector3> originalPositions;
    private List<float> spriteWidths;
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
        if (GameManager.Instance.IsPlaying == false) return;

        for (int i = 0; i < spritesToLoop.Count; i++)
        {
            MoveSprite(spritesToLoop[i], originalPositions[i], spriteWidths[i]);
        }
    }

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