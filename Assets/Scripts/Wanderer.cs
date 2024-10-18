using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Enemy that moves slowly and shoots
public class Wanderer : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float moveSpeed = 2f;
    public float attackRate = 2f;
    private Transform player;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();

        if (Time.time >= nextAttackTime)
        {
            ShootPlayer();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void ShootPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }
}
