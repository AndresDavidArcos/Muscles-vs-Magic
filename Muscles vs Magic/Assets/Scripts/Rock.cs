using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.points += 1;
            Destroy(collision.gameObject);
        }

        if (GameManager.Instance.points == 20)
        {
            Debug.Log("Tu ganas");
        }
    }
}
