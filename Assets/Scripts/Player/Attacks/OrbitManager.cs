using UnityEngine;

public class OrbitManager : MonoBehaviour, IAbility
{
    public GameObject squarePrefab; // The prefab for the square
    public Transform player;         // Reference to the player transform
    public float radius;        // Distance from the player
    public float heightAmplitude; // Amplitude of vertical movement
    public float duration;      // Time the squares stay active
    public float cooldown;     // Time between each spawn
    public int numberOfSquares;  // Number of squares to spawn
    public float rotationSpeed;

    private void SpawnSquares()
    {
        float angleStep = 360f / numberOfSquares; // Calculate angle step for equal spacing

        for (int i = 0; i < numberOfSquares; i++)
        {
            // Calculate the angle for the current square
            float angle = i * angleStep;
            Vector3 position = player.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            GameObject square = Instantiate(squarePrefab, position, Quaternion.identity); // Instantiate the square
            RotationMovement rm = square.GetComponent<RotationMovement>();
            rm.Init(player, radius, heightAmplitude, numberOfSquares, rotationSpeed, i);
            Destroy(square, duration);
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public void Activate()
    {
        SpawnSquares();
    }
}
