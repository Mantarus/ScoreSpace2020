using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public int spawnDelayMid;
    public float spawnDelayDisp;
    public GameObject carPrefab; 

    private DateTime _nextSpawnTime = DateTime.Now;

    private void FixedUpdate()
    {
        var now = DateTime.Now;
        if (_nextSpawnTime < now)
        {
            var spawnDelay = spawnDelayMid + Random.Range(-spawnDelayDisp, spawnDelayDisp) * spawnDelayMid;
            _nextSpawnTime += TimeSpan.FromMilliseconds(spawnDelay);
            
            var spawn = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];
            
            Instantiate(carPrefab, spawn.position, spawn.rotation);
        }
    }
}
