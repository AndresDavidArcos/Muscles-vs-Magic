using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShuriken : MonoBehaviour
{
    public float rotationSpeed = 360f;
    public float shootSpeed = 70f;
    public GameObject enemy; 
    public GameObject ally;
    public LogicPersonajeShuriken playerController;
    public LogicEnemyShuriken enemyController;

    private bool isShot = false;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<LogicPersonajeShuriken>();
        enemyController = GameObject.FindObjectOfType<LogicEnemyShuriken>();
        ally = GameObject.Find("mashle");
        if (ally != null)
        {
            transform.SetParent(ally.transform);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShot)
        {
            ShootShuriken();
        }

        if (isShot)
        {
            MoveShuriken();
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
        transform.SetParent(null);
        StartCoroutine(DestroyAfterTime(4f));
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void MoveShuriken()
    {
        transform.Translate(-Vector3.forward * shootSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyController.ReceiveDamage(20);
            Destroy(gameObject);
        }
    }
}
