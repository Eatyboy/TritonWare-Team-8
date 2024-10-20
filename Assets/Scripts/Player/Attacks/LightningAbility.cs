using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : MonoBehaviour, IAbility
{
    public GameObject lightningPrefab;  // Prefab of the lightning projectile
    public int maxChains = 3;           // Maximum number of enemies it can chain to
    public float chainRange = 5f;       // Range within which it can chain to other enemies
    public float cooldown = 3f;
    public float damage = 3f;

    public float GetCooldown()
    {
        return cooldown;
    }

    public void Activate()
    {
        // Shoot the first lightning bolt (we assume there's a target in front of the player)
        GameObject lightning = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
        LightningProjectile lightningScript = lightning.GetComponent<LightningProjectile>();

        if (lightningScript != null)
        {
            // Set the initial target as the closest enemy in front
            Collider2D firstTarget = FindClosestEnemy();
            if (firstTarget != null)
            {
                lightningScript.Initialize(firstTarget, damage, maxChains, chainRange);
            }
        }
    }

    // Find the first enemy to target
    private Collider2D FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 50f); // Arbitrary range to find enemies
        Collider2D closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = hit;
                }
            }
        }
        return closestEnemy;
    }
}
