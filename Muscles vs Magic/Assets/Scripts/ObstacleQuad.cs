using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriangle : MonoBehaviour
{
     public Transform QuadTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = QuadTransform.position;
        position.x += Mathf.Cos(Time.time) * Time.deltaTime * 100.0f;
        QuadTransform.position = position;
    }
}
