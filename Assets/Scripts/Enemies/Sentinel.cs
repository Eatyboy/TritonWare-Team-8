using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Stationary shooter enemy
public class Sentinel : IEnemy
{

    public GameObject projectilePrefab;
    public float attackRate;
    private float nextAttackTime = 1;

    new void Update()
    {
        base.Update();
        if (Time.time >= nextAttackTime)
        {
            ShootPlayer();
            nextAttackTime = Time.time + attackRate;
        }
    }

    // Move the enemy towards the player
    
    private void ShootPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed
    }
}
