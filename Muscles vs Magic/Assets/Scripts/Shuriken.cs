using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public enum ShurikenType
    {
        Default,
        Boomerang
    }

    public float speed;
    public float rotationSpeed = 360f;
    public ShurikenType shurikenType = ShurikenType.Default;

    private GameObject m_FollowTarget;
    private Rigidbody m_Rb;
    public LogicPersonajeShuriken playerController;
    public System.Action onDestroyCallback;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool returning = false;

    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_FollowTarget = GameObject.Find("mashle");
        playerController = m_FollowTarget.GetComponent<LogicPersonajeShuriken>();
        startPosition = transform.position;

        if (shurikenType == ShurikenType.Boomerang)
        {
            targetPosition = m_FollowTarget.transform.position;
            StartCoroutine(BoomerangMovement());
        }
    }

    void FixedUpdate()
    {
        if (shurikenType == ShurikenType.Default)
        {
            targetPosition = m_FollowTarget.transform.position;
            Vector3 moveTowards = targetPosition - transform.position;
            m_Rb.AddForce(moveTowards.normalized * speed);
        }
        else if (shurikenType == ShurikenType.Boomerang)
        {
            Vector3 moveTowards = returning ? startPosition - transform.position : targetPosition - transform.position;
            m_Rb.AddForce(moveTowards.normalized * speed);
            
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_FollowTarget)
        {
            if(playerController.energy >= playerController.energyRequiredToStealShuriken)
            {
                playerController.ReceiveEnergy(-playerController.energyRequiredToStealShuriken);
                playerController.ReceiveShuriken(1);
            }
            else
            {
                playerController.ReceiveDamage(30);
            }
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

    IEnumerator BoomerangMovement()
    {
        yield return new WaitForSeconds(3f); 
        returning = true;
    }
}
