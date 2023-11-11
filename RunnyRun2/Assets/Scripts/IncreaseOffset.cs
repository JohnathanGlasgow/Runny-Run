using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for changing the offset of a mesh renderer's material.
/// On a material with a repeating texture, this will make the texture appear to loop.
/// </summary>
public class IncreaseOffset : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float speed = 0.1f;

    private void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
           meshRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }       
    }
}
