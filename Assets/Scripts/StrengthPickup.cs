using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPickup : MonoBehaviour
{
    public float strengthIncrease = 1.5f;  // Strength multiplier
    public float duration = 5.0f;          // Duration of the strength buff

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(ApplyStrengthBuff(player));
            }
            // Destroy the pickup after it's collected
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyStrengthBuff(Player player)
    {
        player.damage *= strengthIncrease; // Apply strength buff
        yield return new WaitForSeconds(duration);
        player.damage /= strengthIncrease; // Revert strength buff after duration
    }
}
