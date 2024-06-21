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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(Time.time > shotRateTime && GameManager.Instance.rockAmmo > 0)
            {
                GameManager.Instance.rockAmmo --;
                GameManager.Instance.totalAmmo --;
                
                GameObject newRock;

                newRock = Instantiate(rock, spawnPoint.position, spawnPoint.rotation);

                newRock.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = Time.time + shotRate;

                Destroy(newRock, 5);
            }

            if(GameManager.Instance.totalAmmo == 0)
            {
                GameManager.Instance.ShowDefeatCanvas();
            }
        }

    }
}
