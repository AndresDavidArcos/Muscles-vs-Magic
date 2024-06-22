using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Rigidbody rb; // Referencia al componente Rigidbody del jugador
    public Transform cam; // La cámara principal
    public float speed = 200f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Variable para almacenar el sonido
    public AudioClip collisionSound;
    private AudioSource audioSource;

    private Vector3 direction;
    private bool isFlying = false; // Variable para verificar si el jugador está volando

    void Start()
    {
        // Obtén el componente Rigidbody adjunto al objeto
        rb = GetComponent<Rigidbody>();

        // Ajustar la configuración del Rigidbody
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Obtener o agregar el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = collisionSound;
    }

    void Update()
    {
        // Obtener las entradas del teclado
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Verificar si se presiona la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFlying = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isFlying = false;
        }
    }

    void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            // Calcular el ángulo de rotación basado en la cámara
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Mover al jugador en la dirección de la cámara
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * speed * Time.fixedDeltaTime);
        }

        if (isFlying)
        {
            // Aplicar una fuerza hacia arriba para volar
            rb.AddForce(Vector3.up * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que se colisiona tiene el tag "Sphere"
        if (other.CompareTag("Sphere"))
        {
            // Reproducir el sonido de colisión
            audioSource.Play();
        }
    }
}
