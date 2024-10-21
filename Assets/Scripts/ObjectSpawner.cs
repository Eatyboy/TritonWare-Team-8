using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of objects to spawn (trees, bushes, etc.)
    public int numberOfObjects = 10;    // Number of objects to spawn
    public Vector2 minPosition;         // Minimum position (for random spawn)
    public Vector2 maxPosition;         // Maximum position (for random spawn)

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Pick a random object from the array
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            // Choose a random position within the defined range
            Vector2 randomPosition = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
            // Instantiate the object at the random position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
