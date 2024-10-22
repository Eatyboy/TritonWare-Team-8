using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy that can move in zigzag pattern and shoot burst projectiles
public class Phantom : IEnemy
{
    public int burstCount = 3;  // Number of shots in a burst
    public float burstInterval = 0.5f; // Time between burst shots
    public GameObject projectilePrefab;
    public float attackRate;
    private float nextAttackTime = 0;

    private bool isShooting = false; // Flag to check if shooting is in progress

    new void Update()
    {
        base.Update();
        if (Time.time >= nextAttackTime && !isShooting)
        {
            StartCoroutine(ShootBurst());
            nextAttackTime = Time.time + attackRate; // Set the next attack time
        }
    }

    new void FixedUpdate()
    {
        MoveInPattern();
    }

    private void MoveInPattern()
    {
        // Calculate zigzag movement
        float xOffset = Mathf.Sin(Time.time * ms) * 4f; // Zigzag pattern
        float yOffset = Mathf.Cos(Time.time * ms) * 4f;

        // Get the direction towards the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Combine the zigzag movement with a slight movement towards the player
        Vector2 zigzagMovement = new Vector2(xOffset, yOffset) + directionToPlayer; // Adjust multiplier as needed

        // Move the enemy
        transform.position += (Vector3)zigzagMovement * Time.deltaTime; // Move based on calculated vector
    }

    private IEnumerator ShootBurst()
    {
        isShooting = true; // Set the flag to true while shooting
        for (int i = 0; i < burstCount; i++)
        {
            ShootPlayer(); // Shoot a projectile
            yield return new WaitForSeconds(burstInterval); // Wait for the interval before the next shot
        }
        isShooting = false; // Reset the flag after shooting
    }

    private void ShootPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the projectile's velocity
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed

        // Calculate the angle and rotate the projectile
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Convert to degrees
        angle -= 90f; // Subtract 90 degrees to make the arrow point towards the direction

        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Apply rotation
    }

}
