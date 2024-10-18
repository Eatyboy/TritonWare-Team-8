using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] items;     // Items to spawn
    public Transform player;       // Player reference
    public float spawnRate = 1f;   // Time interval between spawns
    private float nextSpawnTime = 0f;
    public float spawnChance = 0.1f;  // 10% base spawn chance
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        // You could spawn all items at the start if needed
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        foreach (GameObject item in items)
        {
            Instantiate(item, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnItems();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnItems()
    {
        foreach (GameObject item in items)
        {
            if (ShouldSpawn())
            {
                Instantiate(item, GetRandomSpawnPosition(), Quaternion.identity);
            }
        }
    }

    // Determine if an item should spawn based on spawnChance and playerLuck
    bool ShouldSpawn()
    {
        // Calculate the adjusted spawn chance based on player's luck
        float adjustedSpawnChance = spawnChance + (playerScript.luck * 0.01f);  // Each point of luck adds 1% chance
        float randomValue = Random.Range(0f, 1f);  // Generate a random number between 0 and 1

        // Check if the random value is less than the adjusted spawn chance
        return randomValue <= adjustedSpawnChance;
    }

    Vector2 GetRandomSpawnPosition()
    {
        // Generate a random spawn position around the player
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * 15f) + (Vector2)player.position;
        return spawnPosition;
    }
}
