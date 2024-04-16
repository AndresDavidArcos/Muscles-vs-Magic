using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCircleContrary : MonoBehaviour
{
    public Transform ObstacleCircle;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = ObstacleCircle.position;

        position.x -= Mathf.Cos(Time.time) * Time.deltaTime * speed * 70;
        position.z -= Mathf.Sin(Time.time) * Time.deltaTime * speed * 70;

        ObstacleCircle.position = position;
        
    }
}
