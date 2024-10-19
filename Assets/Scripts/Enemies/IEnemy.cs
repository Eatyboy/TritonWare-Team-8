using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy: MonoBehaviour
{
    public int health;
    public int dmg;
    public float ms;
    public float xp;
    protected int spawnDistance = 10;

    [SerializeField] private float invulnerabilityDuration = 1.5f;
    protected float transparencyFlashSpeed = 0.2f;
    protected bool isInvulnerable = false;
    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;

    public Transform player;
    public XPDrop xpPrefab;
    protected Rigidbody2D rb;

    public Sprite sprite1;
    public Sprite sprite2;

    private float spriteSwitchInterval = 0.5f; // Time between switching sprites
    private bool isUsingSprite1 = true; // Tracks which sprite is currently active

    private float nextSpriteSwitchTime = 0f; // Tracks when to switch to the next sprite

    public int getDMG()
    {
        return dmg;
    }

    protected void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
         
        // Randomize spawn position at a certain distance from the player
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnDistance) + (Vector2)player.position;
        transform.position = spawnPosition;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected void Update()
    {
        AlternateSprites();
        
    }

    protected void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * ms * Time.deltaTime);
    }


    protected void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    protected void AlternateSprites()
    {
        if (Time.time >= nextSpriteSwitchTime)
        {
            // Toggle between sprite1 and sprite2
            if (isUsingSprite1)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }

            // Toggle the state
            isUsingSprite1 = !isUsingSprite1;

            // Set the next time to switch sprites
            nextSpriteSwitchTime = Time.time + spriteSwitchInterval;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Check if the enemy has a shield

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

    protected IEnumerator InvulnerabilityCoroutine()
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

    protected void DropXP()
    {
        Instantiate(xpPrefab, transform.position, Quaternion.identity);
    }

    // Destroy enemy when health reaches 0
    protected virtual void Die()
    {
        DropXP();
        Destroy(gameObject);
    }
}
