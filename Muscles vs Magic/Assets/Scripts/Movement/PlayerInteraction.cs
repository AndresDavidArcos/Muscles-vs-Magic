using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RockAmmo"))
        {
            GameManager.Instance.rockAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;
            
            Destroy(other.gameObject);
        }
    }
}
