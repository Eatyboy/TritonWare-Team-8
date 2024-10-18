using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Stationary shooter enemy
public class Sentinel : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackRate = 1.5f;
    private Transform player;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            ShootPlayer();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void ShootPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed
    }
}
