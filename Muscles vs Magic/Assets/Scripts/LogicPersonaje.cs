using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPersonaje : MonoBehaviour
{
    public float velMovement = 5.0f;
    public float velRotate = 200.0f;
    private Animator anim;
    private float initialY;  // Variable para almacenar la posición inicial en Y
    public float x, z;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initialY = transform.position.y;  // Almacenar la posición inicial en Y
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velRotate, 0);

        // Mantener la posición Y constante
        Vector3 movement = new Vector3(0, 0, z * Time.deltaTime * velMovement);
        transform.Translate(movement);

        // Corregir la posición Y después de la traslación
        Vector3 correctedPosition = transform.position;
        correctedPosition.y = initialY;
        transform.position = correctedPosition;

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", z);
    }
}
