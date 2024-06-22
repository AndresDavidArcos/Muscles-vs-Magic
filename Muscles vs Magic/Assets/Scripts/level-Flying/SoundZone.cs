using UnityEngine;

public class SoundZone : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en la zona es el jugador
        if (other.CompareTag("Player"))
        {
            // Reproducir el sonido
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale de la zona es el jugador
        if (other.CompareTag("Player"))
        {
            // Detener el sonido
            audioSource.Stop();
        }
    }
}
