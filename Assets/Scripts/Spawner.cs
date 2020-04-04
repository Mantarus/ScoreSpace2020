using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public int spawnDelayMilliseconds;
    public GameObject carPrefab; 

    private DateTime _lastSpawnTime = DateTime.Now;
    private int _nextSpawnIdx = 0;

    private void FixedUpdate()
    {
        var now = DateTime.Now;
        if ((_lastSpawnTime + TimeSpan.FromMilliseconds(spawnDelayMilliseconds)) < now)
        {
            _lastSpawnTime = now;
            var spawn = spawnPoints[_nextSpawnIdx];
            Instantiate(carPrefab, spawn.position, spawn.rotation);
            _nextSpawnIdx = (_nextSpawnIdx + 1) % spawnPoints.Count;
        }
    }
}
