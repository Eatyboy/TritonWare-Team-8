﻿using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab of the projectile to be fired
    //public Transform firePoint;          // The point from where the projectile is fired
    public float fireInterval;      // Time between each attack

    private float fireTimer = 0;

    void Update()
    {
        fireTimer += Time.deltaTime;

        // Check if it's time to fire
        if (fireTimer >= fireInterval)
        {
            FireProjectile(GameManager.Instance.GetDirectionToMouse(transform));
            SFXManager.Instance.PlayRandomSound(SFXManager.SFX.PLAYER_SHOOT);

            fireTimer = 0f;  // Reset the timer
        }
    }

    // Instantiate and fire the projectile
    void FireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.FromToRotation(Vector3.up, direction));

        // Set projectile's movement direction
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(transform.up); // Pass the direction to the projectile
        }
    }
}