using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
            GameManager.Instance.points += 1;
            Destroy(collision.gameObject);
        }

        if (GameManager.Instance.points == 3)
        {
            GameManager.Instance.ShowVictoryCanvas();
        }
    }
}
