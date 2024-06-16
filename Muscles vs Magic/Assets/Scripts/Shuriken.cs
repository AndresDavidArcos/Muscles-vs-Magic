using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 360f;
    private GameObject m_FollowTarget;
    private Rigidbody m_Rb;
    public LogicPersonajeShuriken playerController;
    public System.Action onDestroyCallback;

    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_FollowTarget = GameObject.Find("mashle");
        playerController = m_FollowTarget.GetComponent<LogicPersonajeShuriken>();
    }

    void FixedUpdate()
    {
        Vector3 moveTowards = m_FollowTarget.transform.position - transform.position;
        m_Rb.AddForce(moveTowards.normalized * speed);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_FollowTarget)
        {
            Debug.Log("Shuriken hit the target.");
            playerController.ReceiveDamage(30);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (onDestroyCallback != null)
        {
            onDestroyCallback();
        }
    }
}
