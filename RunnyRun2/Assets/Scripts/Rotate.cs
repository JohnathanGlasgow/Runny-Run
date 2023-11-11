using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for rotating an object.
/// It should be attached to the object that needs to rotate.
/// </summary>
public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
