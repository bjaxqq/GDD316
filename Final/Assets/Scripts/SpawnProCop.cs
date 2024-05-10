using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProCop : MonoBehaviour
{
    [SerializeField] private GameObject policeCarPrefab; // Prefab for police car

    private bool canSpawnPoliceCar = false;

    void Start()
    {
        StartCoroutine("StartSpawning");
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(10f); // Delay before starting
        canSpawnPoliceCar = true;

        while (true)
        {
            yield return new WaitForSeconds(10f); // Spawn a new police car every 10 seconds
            if (canSpawnPoliceCar)
            {
                SpawnPoliceCar();
            }
        }
    }

    private void SpawnPoliceCar()
    {
        List<Vector3> roadPositions = GetRoadPositions();

        if (roadPositions.Count > 0)
        {
            Vector3 spawnPosition = roadPositions[Random.Range(0, roadPositions.Count)];
            Instantiate(policeCarPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private List<Vector3> GetRoadPositions()
    {
        List<Vector3> roadPositions = new List<Vector3>();
        RaycastHit hit;

        // Cast rays downward to find road positions
        for (float x = -50; x < 50; x += 1)
        {
            for (float z = -50; z < 50; z += 1)
            {
                Vector3 rayOrigin = new Vector3(x, 10f, z);
                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Road"))
                    {
                        roadPositions.Add(hit.point);
                    }
                }
            }
        }

        return roadPositions;
    }
}
