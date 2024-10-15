using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float spawnDistance = 10f; // Distance from the player where the enemy spawns
    public float health = 100f;       // Health of the enemy
    public float moveSpeed = 2f;      // Movement speed towards the player
    public float projectileSpeed = 5f; // Speed of projectiles
    public float attackRate = 2f;     // Time interval between attacks (in seconds)
    public int damage = 10;           // Damage dealt by the enemy's projectile
    public GameObject projectilePrefab; // Prefab for the enemy's projectile
    private Transform player;         // Reference to the player's position
    private float nextAttackTime = 0f; // Time until the next attack

    void Start()
    {
        // Find the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Spawn at a random position around the player within a certain distance
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnDistance) + (Vector2)player.position;
        transform.position = spawnPosition;
    }

    void Update()
    {
        // Move towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackRate; // Reset attack timer
        }
    }

    // Method to shoot a projectile towards the player
    void AttackPlayer()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Get direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Assign velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Optionally, add damage to the projectile if needed
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDamage(damage);
        }
    }

    // Optional: Take damage when hit by player's projectile
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy enemy when health reaches 0
        }
    }
}

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; // Time before the projectile is destroyed
    private int damage;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after its lifetime ends
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
    }
}