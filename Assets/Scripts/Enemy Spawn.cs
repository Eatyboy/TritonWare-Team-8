using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public GameObject specialEnemyPrefab; // Special enemy prefab
    private GameObject specialEnemyInstance; // Instance of the special enemy
    public Transform player;       // Player reference
    public float spawnRate = 3f;   // Time interval between spawns
    private float nextSpawnTime = 0f;
    public float spawnChance;

    private bool specialEnemyActive = false; // Tracks if the special enemy is active

    private void Start()
    {
        // Start coroutine to handle special enemy behavior
        StartCoroutine(SpecialEnemyRoutine());

        // Optional: regular enemy spawning starts immediately
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
    }

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

    // Coroutine to control special enemy appearance and disappearance
    IEnumerator SpecialEnemyRoutine()
    {
        yield return new WaitForSeconds(5f); // Wait 5 seconds before the first appearance

        while (true)
        {
            // Spawn the special enemy if not active
            if (!specialEnemyActive)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                specialEnemyInstance = Instantiate(specialEnemyPrefab, spawnPosition, Quaternion.identity);
                specialEnemyActive = true;
            }

            yield return new WaitForSeconds(10f); // Special enemy stays for 10 seconds

            // Destroy the special enemy and reset the flag
            if (specialEnemyInstance != null)
            {
                Destroy(specialEnemyInstance);
                specialEnemyInstance = null;  // Set the instance reference to null to avoid access to a destroyed object
                specialEnemyActive = false;
            }

            yield return new WaitForSeconds(5f); // Special enemy disappears for 5 seconds
        }
    }
}
