using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShuriken : MonoBehaviour
{
    public float rotationSpeed = 360f;
    public float shootSpeed = 100f;
    public GameObject enemy;
    public GameObject ally;
    public LogicPersonajeShuriken playerController;
    public LogicEnemyShuriken enemyController;

    private bool isShot = false;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<LogicPersonajeShuriken>();
        enemyController = GameObject.FindObjectOfType<LogicEnemyShuriken>();
        enemy = GameObject.Find("Lance_Crown");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShot)
        {
            ShootShuriken();
        }

        RotateShuriken();
    }

    void RotateShuriken()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void ShootShuriken()
    {
        isShot = true;
        playerController.currentAllyShuriken = null;
        playerController.ReceiveShuriken(-1);

        Quaternion characterRotation = playerController.transform.rotation;
        Vector3 shootDirection = characterRotation * Vector3.forward;
        transform.SetParent(null);
        StartCoroutine(DestroyAfterTime(5f));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.velocity = shootDirection * shootSpeed;
    }



    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyController.ReceiveDamage(30);
            Destroy(gameObject);
        }
    }
}
