using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalBurst : MonoBehaviour, IAbility
{
    public GameObject smallProjectilePrefab;  // Prefab of the smaller projectile
    //public Transform firePoint;               // The point from where the projectile is fired
    public float smallProjectileSpeed = 20f;  // Speed of the smaller projectiles
    public float projectileCount = 20f;
    public float cooldown = 10f;

    public float GetCooldown()
    {
        return cooldown; 
    }

    // Instantiate and fire five smaller projectiles
    public void Activate() 
    {
        
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

