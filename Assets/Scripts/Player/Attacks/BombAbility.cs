using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour, IAbility
{
    public GameObject bombPrefab; // Prefab for the bomb projectile
    public float bombSpeed = 15f; // Speed of the bomb projectile
    public float cooldown = 5f;   // Cooldown time for the ability
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public void Activate()
    {
        Vector3 movementDirection = player.GetLastMovementDirection(); // Assume player has a method for movement direction

        // Normalize the direction to ensure consistent speed
        movementDirection = movementDirection.normalized;

        // Instantiate the bomb projectile
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        // Set bomb's velocity in the direction of movement
        rb.velocity = movementDirection * bombSpeed;
    }

}
