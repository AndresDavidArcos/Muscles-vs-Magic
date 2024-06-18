using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravity = -9.81f;
    public float velMovement = 7f;
    public float velRotate = 120f;
    private Animator anim;
    private Rigidbody rb;

    public float jumpHeight = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); 
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.fixedDeltaTime * velRotate, 0);

        Vector3 movement = transform.forward * z * velMovement * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if (IsGrounded())
        {
            Vector3 velocity = rb.velocity;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            else
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            rb.velocity = velocity;
        }
        else
        {
            Vector3 velocity = rb.velocity;
            velocity.y += gravity * Time.fixedDeltaTime;
            rb.velocity = velocity;
        }

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", z);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
