using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speedIncrease = 2.0f;  // Speed boost multiplier
    public float duration = 5.0f;       // Duration of the speed boost

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBuff(player));
            }
            // Destroy the pickup after it's collected
            Destroy(gameObject);
        }
    }

    IEnumerator ApplySpeedBuff(Player player)
    {
        player.moveSpeed *= speedIncrease; // Apply speed boost
        yield return new WaitForSeconds(duration);
        player.moveSpeed /= speedIncrease; // Revert speed boost after duration
    }
}
