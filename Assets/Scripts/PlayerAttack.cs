using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab of the projectile to be fired
    public Transform firePoint;          // The point from where the projectile is fired
    public float fireInterval = 2f;      // Time between each attack

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        // Check if it's time to fire
        if (fireTimer >= fireInterval)
        {
            FireProjectile();
            fireTimer = 0f;  // Reset the timer
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
        transform.up = direction;  // For 2D; use transform.forward for 3D
    }

    // Instantiate and fire the projectile
    void FireProjectile()
    {
        Debug.Log("Firing Projectile!");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Set projectile's movement direction
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(transform.up); // Pass the direction to the projectile
        }
    }
}