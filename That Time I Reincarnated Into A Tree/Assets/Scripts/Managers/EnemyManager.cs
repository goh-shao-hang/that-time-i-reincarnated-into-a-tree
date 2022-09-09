using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemiesToSpawn;

    [SerializeField ]private float spawnDelay = 5f;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;

    private void Start()
    {
        Invoke(nameof(StartSpawning), spawnDelay);
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int enemyToSpawn = Random.Range(0, enemiesToSpawn.Length);
            int spawnPoint = Random.Range(0, spawnPoints.Length);
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            GameObject enemy = Instantiate(enemiesToSpawn[enemyToSpawn], spawnPoints[spawnPoint].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
