using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPersonaje : MonoBehaviour
{
    public float velMovement = 5.0f;
    public float velRotate = 200.0f;
    private Animator anim;
    public float x, z;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        transform.Rotate(0,x*Time.deltaTime*velRotate,0);
        transform.Translate(0,0,z*Time.deltaTime*velMovement);

        anim.SetFloat("VelX",x);
        anim.SetFloat("VelY",z);
    }
}
