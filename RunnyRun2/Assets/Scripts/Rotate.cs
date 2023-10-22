using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script roatates an object along its Y axis
public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
