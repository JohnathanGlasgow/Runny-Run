using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
