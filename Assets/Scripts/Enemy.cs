using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Basic properties
    public int health = 10;
    public int dmg = 3;
    public int moveSpeed = 2;
    public int projectileSpeed = 5;
    public int attackRate = 5;
    public int spawnDistance = 10;

    public GameObject projectilePrefab;
    public Transform player;        // Reference to the player
    private float nextAttackTime = 0f;

    private Rigidbody2D rb;         // Rigidbody for movement

    // Advanced properties (optional)
    public bool canTeleport = false;
    public bool hasShield = false;
    public bool canDodge = false;

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Randomize spawn position at a certain distance from the player
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnDistance) + (Vector2)player.position;
        transform.position = spawnPosition;
    }

    void Update()
    {
        // If the enemy can teleport, start teleporting logic
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

    private string TeleportEnemy()
    {
        throw new System.NotImplementedException();
    }

    // Move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }

    // Shoot projectiles towards the player
    private void AttackPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - transform.position).normalized;
        rbProjectile.velocity = direction * projectileSpeed;
    }

    // Take damage when hit by the player's projectile
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Hit!");
        // If the enemy has a shield, remove it first
        if (hasShield)
        {
            RemoveShield();
            hasShield = false;
            return; // Ignore the damage for this hit
        }

        // Apply damage to the enemy's health
        health -= damageAmount;

        // If health drops to 0 or below, destroy the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to remove the shield (if enemy has one)
    private void RemoveShield()
    {
        // Assuming the shield is a child object of the enemy
        Transform shield = transform.Find("shield");
        if (shield != null)
        {
            Destroy(shield.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the enemy
            collision.GetComponent<Player>().TakeDamage(dmg);

            // Destroy the projectile after dealing damage
            // Destroy(gameObject);

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            float knockbackForce = 5f; // Adjust the knockback force to your preference
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    // Destroy the enemy when health reaches 0
    private void Die()
    {
        Destroy(gameObject);  // Destroy the enemy game object
    }
}
