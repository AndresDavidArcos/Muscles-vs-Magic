using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawn : MonoBehaviour
{
    public GameObject shurikenPrefab;

    private Vector3 spawnAreaMin = new Vector3(-52.3f, 40.39f, 98f);
    private Vector3 spawnAreaMax = new Vector3(17.5f, 40.39f, 160f);

    private List<GameObject> activeShurikens = new List<GameObject>();
    private bool shouldRespawn = true;

    void Start()
    {
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return StartCoroutine(StartFollowingShurikens());
            yield return new WaitForSeconds(30);
            yield return StartCoroutine(StartAirAttack());
            yield return new WaitForSeconds(2);
            yield return StartCoroutine(StartBoomerangAttack());
            yield return new WaitForSeconds(2);
        }
    }

    void PrepareAttack()
    {
        shouldRespawn = false;  
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
        if (shouldRespawn)
        {
            SpawnShuriken(drag, speed);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = spawnAreaMin.y;
        float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        return new Vector3(x, y, z);
    }

    IEnumerator StartFollowingShurikens()
    {
        PrepareAttack();
        shouldRespawn = true;  
        SpawnShuriken(3f, 50f);
        SpawnShuriken(0.2f, 30f);
        SpawnShuriken(0.05f, 70f);
        yield return null;
    }

    IEnumerator StartAirAttack()
    {
        PrepareAttack();

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

        yield return StartCoroutine(SendAirShurikens());
    }

    IEnumerator StartBoomerangAttack()
    {
        PrepareAttack();
        SpawnBoomerangShurikens();

        int iterations = 0;
        int maxIterations = 4;

        while (iterations < maxIterations)
        {
            yield return new WaitForSeconds(5f);
            RespawnBoomerangShurikens();
            iterations++;
        }
    }

    void SpawnBoomerangShurikens()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = GetRandomEdgePosition();
            GameObject shuriken = Instantiate(shurikenPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = shuriken.GetComponent<Rigidbody>();
            Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
            shurikenScript.shurikenType = Shuriken.ShurikenType.Boomerang;
            shurikenScript.speed = 70f;
            rb.drag = 0.5f;
            activeShurikens.Add(shuriken);
        }
    }

    void RespawnBoomerangShurikens()
    {
        PrepareAttack();
        SpawnBoomerangShurikens();
    }

    Vector3 GetRandomEdgePosition()
    {
        int edge = Random.Range(0, 4);
        float x, y = spawnAreaMin.y, z;

        switch (edge)
        {
            case 0: // Left edge
                x = spawnAreaMin.x;
                z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
                break;
            case 1: // Right edge
                x = spawnAreaMax.x;
                z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
                break;
            case 2: // Bottom edge
                x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                z = spawnAreaMin.z;
                break;
            case 3: // Top edge
                x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                z = spawnAreaMax.z;
                break;
            default:
                x = spawnAreaMin.x;
                z = spawnAreaMin.z;
                break;
        }

        return new Vector3(x, y, z);
    }

    IEnumerator SendAirShurikens()
    {
        yield return new WaitForSeconds(5f);
        SendShurikens(2);

        yield return new WaitForSeconds(5f);
        SendShurikens(2);

        while (activeShurikens.Count > 0)
        {
            yield return new WaitForSeconds(2f);
            SendShurikens(1);
        }

        yield return null;
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
        if (shuriken != null)
        {
            Shuriken shurikenScript = shuriken.GetComponent<Shuriken>();
            shurikenScript.onDestroyCallback = null;
            Destroy(shuriken);
        }
    }
}
