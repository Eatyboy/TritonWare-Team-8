using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAbility : MonoBehaviour, IAbility
{
    public GameObject minionPrefab; // Reference to the minion prefab
    public int minionCount = 3; // Number of minions to summon
    public float summonCooldown = 10f; // Cooldown for summoning minions
    public float duration;

    private float summonTimer = 0f;

    public float GetCooldown()
    {
        return summonCooldown;
    }

    public void Activate()
    {
        if (summonTimer > 0)
            return;

        for (int i = 0; i < minionCount; i++)
        {
            // Instantiate the minion at the player's position
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            Destroy(minion, duration);
            // Optionally, set the minion's initial target here
        }

        summonTimer = summonCooldown; // Reset the cooldown
    }

    private void Update()
    {
        if (summonTimer > 0)
            summonTimer -= Time.deltaTime;
    }

    
}
