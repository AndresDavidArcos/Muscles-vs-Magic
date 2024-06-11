using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawn : MonoBehaviour
{
    public GameObject shurikenPrefab;

    private Vector3 spawnAreaMin = new Vector3(-52.3f, 40.39f, 98f);
    private Vector3 spawnAreaMax = new Vector3(17.5f, 40.39f, 160f);

    private List<GameObject> activeShurikens = new List<GameObject>();

    void Start()
    {
        SpawnShuriken(3f, 50f);
        SpawnShuriken(0.2f, 30f);

        // Iniciar el nuevo ataque después de 20 segundos
        StartCoroutine(StartAirAttackAfterDelay(10f));
    }

    void SpawnShuriken(float drag, float speed)
    {
        Vector3 spawnPosition = GetRandomPosition();
        GameObject shuriken = Instantiate(shurikenPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = shuriken.GetComponent<Rigidbody>();
        Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();

        rb.drag = drag;
        shurikenScript.speed = speed;
        shurikenScript.onDestroyCallback = () => StartCoroutine(ReSpawnShuriken(drag, speed));

        activeShurikens.Add(shuriken);
    }

    IEnumerator ReSpawnShuriken(float drag, float speed)
    {
        yield return new WaitForSeconds(2.0f);
        SpawnShuriken(drag, speed);
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = spawnAreaMin.y;
        float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        return new Vector3(x, y, z);
    }

    IEnumerator StartAirAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var shuriken in activeShurikens)
        {
            if (shuriken != null)
            {
                Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
                shurikenScript.onDestroyCallback = null;
                Destroy(shuriken);
            }
        }
        activeShurikens.Clear();

        for (int i = 0; i < 9; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            spawnPosition.y = 68f; 
            Quaternion rotation = Quaternion.Euler(93.20502f, 0, 141.958f);
            GameObject shuriken = Instantiate(shurikenPrefab, spawnPosition, rotation);
            Rigidbody rb = shuriken.GetComponent<Rigidbody>();
            Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
            rb.drag = 0.8f;
            shurikenScript.speed = 0f;
            activeShurikens.Add(shuriken);
        }

        StartCoroutine(SendAirShurikens());
    }

    IEnumerator SendAirShurikens()
    {
        yield return new WaitForSeconds(5f);
        SendShurikens(2);

        yield return new WaitForSeconds(5f);
        SendShurikens(1);

        while (activeShurikens.Count > 0)
        {
            yield return new WaitForSeconds(2f);
            SendShurikens(1);
        }
    }

    void SendShurikens(int count)
    {
        int sent = 0;
        foreach (var shuriken in activeShurikens.ToArray())
        {
            if (sent >= count) break;

            if (shuriken != null)
            {
                Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
                if (shurikenScript.speed == 0f)
                {
                    shurikenScript.speed = 90f;
                    Rigidbody rb = shuriken.GetComponent<Rigidbody>();
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                    activeShurikens.Remove(shuriken);
                    StartCoroutine(DestroyShurikenAfterDelay(shuriken, 2f));
                    sent++;
                }
            }
        }
    }

    IEnumerator DestroyShurikenAfterDelay(GameObject shuriken, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(shuriken != null)
        {
            Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
            shurikenScript.onDestroyCallback = null;
            Destroy(shuriken);
        }

    }
}
