using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for destroying objects when they collide with a trigger.
/// </summary>

public class DestroyOnTrigger : MonoBehaviour
{
    // The tag of the object you want to destroy.
    public string tagFilter;

    /// <summary>
    /// This method is called when the object collides with a trigger.
    /// </summary>
    /// <param name="other">The collider of the object that collided with this object.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagFilter))
        {
            // if this object has the ring tag, destroy it
            if (CompareTag("Ring"))
            {
                Destroy(gameObject);
            }
            // otherwise its an obstacle so the parent object needs to be destroyed
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
