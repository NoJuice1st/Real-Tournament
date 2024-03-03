using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Components")]
    public GameObject prefab;

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

    private int enemiesLeft;
    private int wave = 0;

    async void Start()
    {
        foreach (var count in enemiesPerWave)
        {
            enemiesLeft = count;
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
        var point = spawnPoints[Random.Range(0, spawnPoints.Count)];

        Instantiate(prefab, point.position, point.rotation);
        onSpawn.Invoke();
    }
}
