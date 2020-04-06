using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public List<GameObject> carPrefabs; 
    public List<Transform> spawnPoints;
    public int spawnDelayMid;
    public float spawnDelayDisp;

    private DateTime _nextSpawnTime = DateTime.Now;

    private void FixedUpdate()
    {
        var now = DateTime.Now;
        if (_nextSpawnTime < now)
        {
            var spawnDelay = spawnDelayMid + Random.Range(-spawnDelayDisp, spawnDelayDisp) * spawnDelayMid;
            _nextSpawnTime = DateTime.Now + TimeSpan.FromMilliseconds(spawnDelay);
            
            var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
            var carPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];
            
            Instantiate(carPrefab, spawn.position, spawn.rotation);
        }
    }
}
