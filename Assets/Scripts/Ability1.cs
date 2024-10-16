using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1 : MonoBehaviour
{
    public GameObject smallProjectilePrefab;  // Prefab of the smaller projectile
    //public Transform firePoint;               // The point from where the projectile is fired
    public float smallProjectileCooldown = 10f; // Cooldown for firing small projectiles
    public float smallProjectileSpeed = 20f;  // Speed of the smaller projectiles
    public float projectileCount = 20f;

    private float smallProjectileTimer = 10;

    void Update()
    {
        smallProjectileTimer += Time.deltaTime;

        // Check for E key press and cooldown for firing small projectiles
        if (Input.GetKeyDown(KeyCode.E) && smallProjectileTimer >= smallProjectileCooldown)
        {
            FireSmallProjectiles();
            smallProjectileTimer = 0f;  // Reset the cooldown timer
        }

        AimAtMouse();
    }

    void AimAtMouse()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set the z position to camera's near clip plane
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction to the mouse
        Vector2 direction = new Vector2(worldMousePosition.x - transform.position.x, worldMousePosition.y - transform.position.y);

        // Rotate the player or fire point to face the mouse
        transform.up = direction;
    }

    // Instantiate and fire five smaller projectiles
    void FireSmallProjectiles()
    {
        Debug.Log("Firing Small Projectiles!");
        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the offset angle for spreading the projectiles
            float angle = i * (360f / projectileCount); // Spread them out evenly
            Quaternion rotation = Quaternion.Euler(0, 0, angle); // Adjust based on 2D or 3D

            GameObject smallProjectile = Instantiate(smallProjectilePrefab, transform.position, transform.rotation * rotation);

            // Set the direction and speed for the smaller projectile
            Projectile smallProjectileScript = smallProjectile.GetComponent<Projectile>();
            if (smallProjectileScript != null)
            {
                smallProjectileScript.SetDirection(rotation * transform.up); // Use the spread direction
            }
        }
    }
}

