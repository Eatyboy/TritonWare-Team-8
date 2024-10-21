using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgBoostPickup : MonoBehaviour
{
    public float damageMultiplier = 2.0f; // Damage boost multiplier
    public float duration = 5.0f;         // Duration of the damage boost

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(ApplyDamageBoost(player));
            }
            // Destroy the pickup after it's collected
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyDamageBoost(Player player)
    {
        player.damage *= damageMultiplier; // Apply damage boost
        yield return new WaitForSeconds(duration);
        player.damage /= damageMultiplier; // Revert damage boost after duration
    }
}
