using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Components")]
    public GameObject prefab;
    public GameObject prefab2;

    public List<Transform> spawnPoints;
    public List<int> enemiesPerWave;
    [Space(10)]

    [Range(0f, 10f)]public float timeBetweenWaves = 10f;
    [Range(0f, 10f)]public float timeInterval = 2f;

    [Header("Events")]
    [Space(10)]
    public UnityEvent onSpawn;
    public UnityEvent<int> onWaveStart;
    public UnityEvent<int> onWaveEnd;
    public UnityEvent onWavesCleared;

    private int originalEnemyAmount;
    private int enemiesLeft;
    private int wave = 0;

    async void Start()
    {
        foreach (var count in enemiesPerWave)
        {
            enemiesLeft = count;
            originalEnemyAmount = enemiesLeft;
            onWaveStart.Invoke(wave);

            while (enemiesLeft > 0)
            {
                await new WaitForSeconds(timeInterval);
                Spawn();
                enemiesLeft--;
            }

            onWaveEnd.Invoke(wave);
            wave++;
            await new WaitForSeconds(timeBetweenWaves);
        }

        onWavesCleared.Invoke();
    }

    public void Spawn()
    {
        var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];

        if(originalEnemyAmount >= 5)
        {
            if(UnityEngine.Random.Range(0f, 10f) > 5)
            {
                Instantiate(prefab2, point.position, point.rotation);
                onSpawn.Invoke();
            }
            else
            {
                Instantiate(prefab, point.position, point.rotation);
                onSpawn.Invoke();
            }
        }
        else
        {
            Instantiate(prefab, point.position, point.rotation);
            onSpawn.Invoke();
        }
    }
}
