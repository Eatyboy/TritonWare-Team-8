using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs: Phantom, Warlord, Sentinel, Wanderer, Boss, Cloud and CycloneCloud
    public float spawnRate = 3f;      // Time interval between spawns
    private float nextSpawnTime = 0f;
    public Transform player;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomIndex];
        Instantiate(selectedEnemy, GetRandomSpawnPosition(), Quaternion.identity);
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * 10f) + (Vector2)player.position;
        return spawnPosition;
    }
}
