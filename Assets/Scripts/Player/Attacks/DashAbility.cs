using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    public float dashSpeedMultiplier = 2f;  // Multiplier for dash speed
    public float dashDuration = 0.3f;       // Duration of the dash
    public float cooldown = 5f;             // Cooldown between dashes

    private Player player;
    private Rigidbody2D rb;
    private bool isDashing = false;         // To track if player is currently dashing

    public float GetCooldown()
    {
        return cooldown;
    }

    void Start()
    {
        player = GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        // Dash if not currently dashing
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        Debug.Log("Should dash");
        Vector3 dashDirection = player.GetMovementDirection().normalized;

        if (dashDirection == Vector3.zero)
        {
            dashDirection = player.GetLastMovementDirection(); // Use last direction instead
        }

        // Apply the dash speed
        Vector3 originalVelocity = rb.velocity;
        rb.velocity = dashDirection * player.moveSpeed * dashSpeedMultiplier;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Restore normal speed
        rb.velocity = originalVelocity;

        isDashing = false;  // Reset dashing flag
    }
}
