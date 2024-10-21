using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBomb : MonoBehaviour
{
    public GameObject clusterProjectilePrefab; // Prefab of the smaller projectiles
    public int clusterCount;              // Number of smaller projectiles
    public float clusterSpeed;          // Speed of the smaller projectiles
    public float explosionRadiusf;        // Radius of the cluster spread
    public int dmg;
    public float cooldown;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return; // Ignore collision with player
        }
        else if (collision.CompareTag("Enemy"))
        {
            // Player projectile hits the enemy
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(dmg);
            DamagePopupManager.Instance.NewPopup(dmg, collision.transform.position);
            // Trigger cluster release upon impact
            Explode();
        }
    }

    private void Explode()
    {
        // Create the cluster projectiles in a radial spread
        for (int i = 0; i < clusterCount; i++)
        {
            float angle = i * (360f / clusterCount); // Spread them out evenly
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject clusterProjectile = Instantiate(clusterProjectilePrefab, transform.position, rotation);
            Rigidbody2D rb = clusterProjectile.GetComponent<Rigidbody2D>();

            // Apply velocity to the smaller projectiles
            Vector2 direction = rotation * Vector2.up; // Adjust direction based on angle
            rb.velocity = direction * clusterSpeed;
        }

        // Optionally, destroy the bomb after it explodes
        Destroy(gameObject);
    }
}
