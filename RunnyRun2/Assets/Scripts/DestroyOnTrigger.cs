using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public string tagFilter;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // log out the tag of the object that collided with this object
        Debug.Log(other.tag);
        if (other.CompareTag(tagFilter))
        {
            // if this object has the ring tag, destroy it
            if (CompareTag("Ring"))
            {
                Destroy(gameObject);
            }
            else
            {
                // destroy my parent object
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
