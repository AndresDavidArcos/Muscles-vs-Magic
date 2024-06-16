using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPersonajeAlejandro : MonoBehaviour
{
    public float velMovement = 5.0f;
    public float velRotate = 200.0f;
    private Animator anim;
    private Rigidbody rb;
    public float x, z;
    private float initialY; // Variable para almacenar la posición inicial en Y

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Asegúrate de que el Rigidbody esté configurado correctamente
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        initialY = transform.position.y; // Almacenar la posición inicial en Y
    }

    // FixedUpdate se llama en intervalos de tiempo fijos y es mejor para la física
    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // Rotar el personaje
        float rotation = x * velRotate * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

        // Movimiento usando Rigidbody
        Vector3 movement = transform.forward * z * velMovement * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Actualizar animaciones
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", z);
    }
}
