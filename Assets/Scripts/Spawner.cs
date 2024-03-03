using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public List<Transform> spawnPoints;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        var point = spawnPoints[Random.Range(0, spawnPoints.Count)];

        Instantiate(prefab, point.position, point.rotation);
    }
}
