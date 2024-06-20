using System.Collections;
using UnityEngine;

public class EnergySpawn : MonoBehaviour
{
    public GameObject energyCubePrefab; // El prefab del EnergyCube
    public Vector3 spawnAreaMin = new Vector3(-52.3f, 40.39f, 98f);
    public Vector3 spawnAreaMax = new Vector3(17.5f, 40.39f, 160f);

    private GameObject currentEnergyCube;
    private float spawnDelay = 5f; // Retardo de respawn
    private bool isSpawning = false; // Bandera para evitar múltiples coroutines

    void Start()
    {
        SpawnEnergyCube();
    }

    void Update()
    {
        if (currentEnergyCube == null && !isSpawning)
        {
            StartCoroutine(SpawnEnergyCubeAfterDelay());
        }
    }

    private void SpawnEnergyCube()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            spawnAreaMin.y,
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        currentEnergyCube = Instantiate(energyCubePrefab, randomPosition, Quaternion.identity);
    }

    private IEnumerator SpawnEnergyCubeAfterDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnergyCube();
        isSpawning = false;
    }

    public void DestroyEnergyCube()
    {
        currentEnergyCube = null;
    }
}
