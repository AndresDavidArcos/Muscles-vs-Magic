using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 30.0f; // Velocidad de rotación en grados por segundo

    // Update is called once per frame
    void Update()
    {
        // Rotar el objeto alrededor del eje Y
        transform.Rotate(Vector3.one * rotationSpeed*3 * Time.deltaTime);
    }
}
