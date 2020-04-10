using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public int cacheSize;
    
    public List<GameObject> carPrefabs;
    public List<Transform> spawnPoints;
    
    public int spawnDelayMid;
    public float spawnDelayDisp;

    private readonly Queue<GameObject> _carCache = new Queue<GameObject>();
    private DateTime _nextSpawnTime = DateTime.Now;

    private void Start()
    {
        for (int i = 0; i < cacheSize; i++)
        {
            var car = GetRandomCarInstance();
            car.SetActive(false);
            _carCache.Enqueue(car);
        }
    }

    private void FixedUpdate()
    {
        var now = DateTime.Now;
        if (_nextSpawnTime < now)
        {
            var spawnDelay = spawnDelayMid + Random.Range(-spawnDelayDisp, spawnDelayDisp) * spawnDelayMid;
            _nextSpawnTime = DateTime.Now + TimeSpan.FromMilliseconds(spawnDelay);
            
            var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
            SpawnCar(spawn);
        }
    }

    private GameObject GetRandomCarInstance()
    {
        var carPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];
        var car = Instantiate(carPrefab);
        car.GetComponent<CarMover>().spawner = this;
        return car;
    }

    public void StashCar(GameObject car)
    {
        car.SetActive(false);
        _carCache.Enqueue(car);
        cacheSize = _carCache.Count;
    }

    public void SpawnCar(Transform spawn)
    {
        GameObject car;
        if (_carCache.Count > 0)
        {
            car = _carCache.Dequeue();
        }
        else
        {
            car = GetRandomCarInstance();
        }
        car.transform.position = spawn.position;
        car.transform.rotation = spawn.rotation;
        car.SetActive(true);
    }
}
