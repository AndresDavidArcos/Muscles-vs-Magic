using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCube : MonoBehaviour
{
    public GameObject m_FollowTarget;
    public LogicPersonajeShuriken playerController;
    public float rotationSpeed = 360f;

    void Start()
    {
        m_FollowTarget = GameObject.Find("mashle");
        playerController = m_FollowTarget.GetComponent<LogicPersonajeShuriken>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_FollowTarget)
        {
            playerController.ReceiveEnergy(1);
            playerController.ReceiveDamage(-10);
            Destroy(gameObject);
        }
    }
}
