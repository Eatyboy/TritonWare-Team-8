using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    public GameObject squarePrefab; // The prefab for the square
    public Transform player;         // Reference to the player transform
    public float radius = 2f;        // Distance from the player
    public float heightAmplitude = 2f; // Amplitude of vertical movement
    public float duration = 5f;      // Time the squares stay active
    public float cooldown = 10f;     // Time between each spawn
    public int numberOfSquares = 2;  // Number of squares to spawn
    public float rotationSpeed = 5f;

    private GameObject[] squares;    // Array to hold the square instances
    private float timer;             // Timer for managing ability timing
    private bool isActive = false;   // To track if squares are currently active

    private void Start()
    {
        squares = new GameObject[numberOfSquares]; // Initialize the array for squares
    }

    private void Update()
    {
        if (!isActive)
        {
            timer += Time.deltaTime;

            if (timer >= cooldown)
            {
                SpawnSquares();
                timer = 0f; // Reset the timer after spawning
            }
        }
        else
        {
            MoveSquares();
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                DestroySquares();
                timer = 0f; // Reset the timer after destruction
            }
        }
    }

    private void SpawnSquares()
    {
        float angleStep = 360f / numberOfSquares; // Calculate angle step for equal spacing

        for (int i = 0; i < numberOfSquares; i++)
        {
            // Calculate the angle for the current square
            float angle = i * angleStep;
            Vector3 position = player.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            squares[i] = Instantiate(squarePrefab, position, Quaternion.identity); // Instantiate the square
        }

        isActive = true; // Mark squares as active
    }

    private void MoveSquares()
    {
        float time = Time.time; // Get the current time

        for (int i = 0; i < squares.Length; i++)
        {
            float angle = time * rotationSpeed * Mathf.Deg2Rad + (i * Mathf.PI * 2 / numberOfSquares);
            // Calculate position based on angle, with vertical movement included
            Vector3 position = player.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * heightAmplitude, Mathf.Sin(angle) * radius);
            squares[i].transform.position = position; // Update square position
        }
    }

    private void DestroySquares()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] != null)
            {
                Destroy(squares[i]); // Destroy the square
            }
        }
        isActive = false; // Mark squares as inactive
    }
}
