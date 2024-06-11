using UnityEngine;

public class LogicPersonajeShuriken : MonoBehaviour
{
    public HudManager hudManager;
    public float velMovement = 20;
    public float velRotate = 120;
    private Animator anim;
    private Rigidbody rb;
    public int health = 200;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        hudManager.UpdateHealthText("Vida: " + health);
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

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        hudManager.UpdateHealthText("Vida: " + health);
    }
}
