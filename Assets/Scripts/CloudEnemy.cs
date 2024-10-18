using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : MonoBehaviour
{
    public GameObject beamPrefab;
    public GameObject waterPelletPrefab; // Reference to the water pellet prefab
    public float pelletSpawnInterval = 1.0f; // Time interval between pellet spawns
    public float attackInterval = 3.0f; // Time interval between streak attacks
    public float screenFlashDuration = 0.5f; // Duration of the flash effect
    public float attackDuration = 0.5f; // Duration of the attack
    public float diagonalStreakLength = 5.0f; // Length of the streak
    public float pelletSpeed = 5f; // Speed of the water pellets
    public int pelletDamage = 5; // Damage of the water pellets
    private int beamDuration = 1;

    public float hoverAmplitude = 0.2f; // Amplitude of the hovering motion
    public float hoverSpeed = 2f; // Speed of the hovering motion
    public float horizontalMovementAmplitude = 0.5f; // Amplitude of horizontal movement
    public float horizontalMovementSpeed = 2f; // Speed of horizontal movement

    private float initialY; // Initial Y position of the cloud
    private Transform player; // Reference to the player's transform

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y + 30; // Store the initial Y position

        InvokeRepeating(nameof(SpawnWaterPellet), 0, pelletSpawnInterval);
        InvokeRepeating(nameof(FlashAttack), attackInterval, attackInterval);
    }

    private void Update()
    {
        Hover();
        MoveHorizontally();
    }

    private void Hover()
    {
        // Calculate the new Y position based on the hover offset
        float newY = (player.position.y + 9) + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void MoveHorizontally()
    {
        // Get the player's X position
        float playerX = player.position.x;

        // Calculate the new X position based on horizontal movement amplitude
        float newX = playerX + Mathf.Sin(Time.time * horizontalMovementSpeed) * horizontalMovementAmplitude;

        // Update the cloud's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void SpawnWaterPellet()
    {
        GameObject pellet = Instantiate(waterPelletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -pelletSpeed); // Move downwards
        pellet.GetComponent<Projectile>().dmg = pelletDamage; // Set the damage
    }

    private void FlashAttack()
    {
        StartCoroutine(FlashAndAttack());
    }

    private IEnumerator FlashAndAttack()
    {
        yield return StartCoroutine(FlashDiagonalStreak());
        // Call to spawn the beam after the flash
        SpawnBeam();
    }

    private IEnumerator FlashDiagonalStreak()
    {
        // Camera.main.backgroundColor = Color.red; // Flash effect
        yield return new WaitForSeconds(screenFlashDuration);

    }

    private void SpawnBeam()
    {

        // Calculate direction to the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Fire down-right (45 degrees) if player is on the right
        if (directionToPlayer.x > 0 && directionToPlayer.y < 0)
        {
            // Create the beam slightly offset to appear as if fired diagonally
            Vector3 offset = new Vector3(1f, -1f, 0).normalized; // Down-right
            GameObject beam = Instantiate(beamPrefab, transform.position + offset, Quaternion.Euler(0, 0, 45)); // Rotate 45 degrees
            beam.GetComponent<Beam>().Fire(offset, diagonalStreakLength, beamDuration); // Fire direction
        }
        // Fire down-left (270 degrees) if player is on the left
        else if (directionToPlayer.x < 0 && directionToPlayer.y < 0)
        {
            // Create the beam slightly offset to appear as if fired diagonally
            Vector3 offset = new Vector3(-1f, -1f, 0).normalized; // Down-left
            GameObject beam = Instantiate(beamPrefab, transform.position + offset, Quaternion.Euler(0, 0, 270)); // Rotate 270 degrees
            beam.GetComponent<Beam>().Fire(offset, diagonalStreakLength, beamDuration); // Fire direction
        }
    }

}
