using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy to spawn
    public GameObject specialEnemy;
    public Transform player;       // Player reference
    public float spawnRate = 3f;   // Time interval between spawns
    private float nextSpawnTime = 0f;
    public float spawnChance;

    private void Start()
    {
        // Set spawn position to the top left corner of the screen
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));

        Instantiate(specialEnemy, spawnPosition, Quaternion.identity);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * 10f) + (Vector2)player.position;
        return spawnPosition;
    }
}
