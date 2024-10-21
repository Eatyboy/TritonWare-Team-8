using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStrike : MonoBehaviour, IAbility
{
    public float teleportRange = 5f;
    public float aoeRadius = 2f;
    public float aoeDamage = 50f;
    public GameObject teleportShadowPrefab;  // Prefab of the shadow/indicator
    private GameObject activeTeleportShadow; // Reference to the active shadow object
    private Player player;

    public float cooldown = 3f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        player = GetComponent<Player>();

        // Instantiate the teleport shadow once, and hide it initially
        if (teleportShadowPrefab != null)
        {
            activeTeleportShadow = Instantiate(teleportShadowPrefab, transform.position, Quaternion.identity);
            activeTeleportShadow.SetActive(true);
        }
    }

    public void Activate()
    {
        if (cooldownTimer > 0)
        {
            return; // Ability is on cooldown, do nothing
        }

        Vector3 teleportDirection = (player.GetLastMovementDirection() != Vector3.zero) ? (Vector3)player.GetLastMovementDirection() : transform.up;

        // Calculate target position based on the last movement direction
        Vector3 targetPosition = transform.position + teleportDirection * teleportRange;

        // Perform teleport
        transform.position = targetPosition;

        // Deal damage in the AOE radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        foreach (Collider2D hitCollider in hitEnemies)
        {
            IEnemy enemy = hitCollider.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)aoeDamage);
            }
        }

        // Start cooldown
        cooldownTimer = cooldown;

        // Hide the teleport shadow while on cooldown
        activeTeleportShadow.SetActive(false);

        player.TakeDamage(0);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        // Update cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= dt;
            if (cooldownTimer <= 0)
            {
                // Off cooldown, show the teleport shadow
                activeTeleportShadow.SetActive(true);
            }
        }

        // Update shadow position if the ability is off cooldown
        if (cooldownTimer <= 0 && activeTeleportShadow != null)
        {
            Vector3 teleportDirection = (player.GetLastMovementDirection() != Vector3.zero) ? (Vector3)player.GetLastMovementDirection() : transform.up;
            Vector3 targetPosition = transform.position + teleportDirection * teleportRange;

            // Update the shadow position to indicate where the teleport will land
            activeTeleportShadow.transform.position = targetPosition;
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
