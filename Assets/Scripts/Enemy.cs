using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Basic properties
    public float health = 100;
    public float damage = 10;
    public float moveSpeed = 2f;
    public float attackRate = 2f;
    public float projectileSpeed = 5f;
    public float spawnDistance = 10;
    public float nextAttackTime = 0f;

    // Advanced properties (optional)
    public bool canTeleport = false;
    public bool hasShield = false;
    public bool canDodge = false;

    public GameObject projectilePrefab; // Enemy projectile or bullet
    public GameObject shield; // Shield  if needed
    public Transform player;        // Reference to the player

    private Rigidbody2D rb;         // Rigidbody for movement

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Randomize spawn position at a certain distance from the player
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnDistance) + (Vector2)player.position;
        transform.position = spawnPosition;

        // Optional: Activate shield for advanced enemies
        if (hasShield)
        {
            ActivateShield();
        }
}

    void Update()
    {
        if (canTeleport)
        {
            StartCoroutine(TeleportEnemy());
        }
        else
        {
            MoveTowardsPlayer();
        }

        // Attack the player at regular intervals
        if (Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackRate;
        }
}

    // Move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }

    // Teleport the enemy to a new random location around the player
    private IEnumerator TeleportEnemy()
    {
        yield return new WaitForSeconds(3f); // Teleport every 3 seconds
        Vector2 teleportPosition = (Random.insideUnitCircle.normalized * spawnDistance) + (Vector2)player.position;
        transform.position = teleportPosition;
    }

    // Shoot projectiles towards the player
    public void AttackPlayer()
    {
        if (canDodge)
        {
            //DodgeProjectiles(); // Advanced behavior to dodge player bullets
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - transform.position).normalized;
        rbProjectile.velocity = direction * projectileSpeed;
}

    // Optional: Dodge the player's projectiles (for advanced enemies)
    private void DodgeProjectiles()
    {
        // Implement dodging logic (e.g., moving left/right rapidly)
        Vector2 dodgeDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.MovePosition((Vector2)transform.position + dodgeDirection * moveSpeed * 1.5f * Time.deltaTime);
    }

    // Take damage when hit by the player's projectile
    public void TakeDamage(float damageAmount)
    {
        // Check if the enemy has a shield
        if (hasShield)
        {
            // Remove the shield
            RemoveShield();
            hasShield = false; // Update the shield status
            return; // Ignore damage for this hit, but the shield is now removed
        }

        // If no shield, apply damage to the enemy's health
        health -= damageAmount;

        // Check if health is 0 or less and destroy the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to remove or destroy the shield
    private void RemoveShield()
    {
        // Assuming the shield is a child object of the enemy, destroy it
        Transform shield = transform.Find("shield"); // Use the name of your shield prefab
        if (shield != null)
        {
            Destroy(shield.gameObject); // Remove the shield from the enemy
        }
    }

    // Optional: Activate shield for advanced enemies
     public void ActivateShield()
     {
         Instantiate(shield, transform.position, Quaternion.identity, transform);
     }
    

    // Destroy enemy when health reaches 0
    private void Die()
    {
        Destroy(gameObject);
    }
}