using UnityEngine;

public class LogicPersonaje : MonoBehaviour
{
    public float velMovement = 20f;
    public float velRotate = 120f;
    private Animator anim;
    private Rigidbody rb;

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


        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", z);
    }
}
