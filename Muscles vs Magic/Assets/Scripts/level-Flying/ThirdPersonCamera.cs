using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // El transform del jugador
    public Vector3 offset; // El desplazamiento de la cámara respecto al jugador
    public float sensitivity = 5f; // Sensibilidad del mouse

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor al centro de la pantalla
    }

    void LateUpdate()
    {
        // Obtener el movimiento del mouse
        currentRotationX += Input.GetAxis("Mouse X") * sensitivity;
        currentRotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotationY = Mathf.Clamp(currentRotationY, -35, 60); // Limitar la rotación vertical

        // Aplicar la rotación al jugador y la cámara
        player.rotation = Quaternion.Euler(0f, currentRotationX, 0f);
        transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);

        // Posicionar la cámara con el offset
        transform.position = player.position - transform.forward * offset.z + transform.up * offset.y;
    }
}
