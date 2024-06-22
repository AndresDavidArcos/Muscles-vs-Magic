using UnityEngine;

public class ExplosionOnContact : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab del sistema de partículas para la explosión

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra en contacto es el jugador
        {
            // Instancia el sistema de partículas en la posición de la esfera
            Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Notifica al GameManager
            GameManagerFly.instanceFly.SphereTouched();

            // Desactiva la esfera
            gameObject.SetActive(false);
        }
    }
}
