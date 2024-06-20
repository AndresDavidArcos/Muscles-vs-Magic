using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint;

    public GameObject rock;

    public float shotForce = 1500f;
    public float shotRate = 0.8f;

    private float shotRateTime = 0;

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(Time.time > shotRateTime && GameManager.Instance.rockAmmo > 0)
            {
                GameManager.Instance.rockAmmo --;
                
                GameObject newRock;

                newRock = Instantiate(rock, spawnPoint.position, spawnPoint.rotation);

                newRock.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = Time.time + shotRate;

                Destroy(newRock, 5);
            }
        }

    }
}
