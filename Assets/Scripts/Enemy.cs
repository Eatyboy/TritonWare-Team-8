using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Basic properties
    public int health = 100;
    public int damage = 10;
    public int moveSpeed = 2;
    public int projectileSpeed = 5;
    public int attackRate = 2;
    public int spawnDistance = 10;
    public int xp; // adjust this for different enemy

    // Advanced properties (optional)
    public bool hasShield = false;
    public bool canDodge = false;

    // Invulnerability shit
    [SerializeField] private float invulnerabilityDuration = 1.5f; // Duration of invulnerability
    private float transparencyFlashSpeed = 0.2f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    //public GameObject projectile; // Enemy projectile or bullet
    ////public GameObject shield; // Shield  if needed
    public GameObject projectilePrefab;
    public Transform player;        // Reference to the player
    private float nextAttackTime = 0f;
    public XPDrop xpPrefab; 

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
            //ActivateShield();
        }
        //player = FindObjectByType<Player>().transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Attack the player at regular intervals
        if (Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    // Move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
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
    public void TakeDamage(int damageAmount)
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
        if (!isInvulnerable)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(InvulnerabilityCoroutine());
            }
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
    //public void ActivateShield()
    //{
    //    Instantiate(shield, transform.position, Quaternion.identity, transform);
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the enemy
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);

            // Destroy the projectile after dealing damage
            // Destroy(gameObject);

            float knockbackForce = 5f; // Adjust the knockback force to your preference
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            // Flashing effect: toggle transparency on and off
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            yield return new WaitForSeconds(transparencyFlashSpeed);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(transparencyFlashSpeed);

            elapsedTime += transparencyFlashSpeed * 2; // Because of two WaitForSeconds
        }


        isInvulnerable = false;
    }

    private void DropXP()
    {
        Instantiate(xpPrefab, transform.position, Quaternion.identity);
	}

    // Destroy enemy when health reaches 0
    private void Die()
    {
        DropXP();
        Destroy(gameObject);
    }
}