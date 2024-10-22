using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : MonoBehaviour
{
    public GameObject beamPrefab;
    public GameObject waterPelletPrefab; // Reference to the water pellet prefab
    public float pelletSpawnInterval; // Time interval between pellet spawns
    public float pelletSpeed; // Speed of the water pellets
    public int pelletDamage; // Damage of the water pellets

    public float hoverAmplitude = 0.2f; // Amplitude of the hovering motion
    public float hoverSpeed = 2f; // Speed of the hovering motion
    public float horizontalMovementAmplitude = 0.5f; // Amplitude of horizontal movement
    public float horizontalMovementSpeed = 2f; // Speed of horizontal movement

    private float initialY; // Initial Y position of the cloud
    private Transform player; // Reference to the player's transform

    public Sprite sprite1;
    public Sprite sprite2;

    private float spriteSwitchInterval = 0.5f; // Time between switching sprites
    private bool isUsingSprite1 = true; // Tracks which sprite is currently active
    protected SpriteRenderer spriteRenderer;

    private float nextSpriteSwitchTime = 0f; // Tracks when to switch to the next sprite

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y + 30; // Store the initial Y position

        InvokeRepeating(nameof(SpawnWaterPellet), 0, pelletSpawnInterval);
    }

    private void Update()
    {
        Hover();
        MoveHorizontally();
        AlternateSprites();

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

}
