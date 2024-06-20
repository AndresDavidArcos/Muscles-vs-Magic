using Unity.VisualScripting;
using UnityEngine;

public class LogicPersonajeShuriken : MonoBehaviour
{
    public HudManager hudManager;
    public float velMovement = 20;
    public float velRotate = 120;
    private Animator anim;
    private Rigidbody rb;
    public int health = 200;
    public int energy = 0;
    public int shurikens = 0;
    public int energyRequiredToStealShuriken = 2;
    public GameObject defeatCanvas;
    public GameObject allyShurikenPrefab; 
    public GameObject currentAllyShuriken;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        hudManager.UpdateHealthText("Vida: " + health);
        hudManager.UpdateEnergyText("Energia: " + energy);
        hudManager.UpdateShurikensText("Shurikens: " + shurikens);

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
        if(health < 0)
        {
            defeatCanvas.SetActive(true);
        }
    }

    public void ReceiveEnergy(int energyPower)
    {
        energy += energyPower;
        hudManager.UpdateEnergyText("Energia: " + energy);
    }

    public void ReceiveShuriken(int shurikensReceived)
    {
        shurikens += shurikensReceived;
        CreateAllyShuriken();
        hudManager.UpdateShurikensText("Shurikens: " + shurikens);
    }

    public void CreateAllyShuriken()
    {
        if (shurikens >= 1 && currentAllyShuriken == null)
        {
            currentAllyShuriken = Instantiate(allyShurikenPrefab);
        }
    }

}
