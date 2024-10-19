using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy that can shoot beam and projectile spread
public class Warlord : IEnemy
{
    public GameObject aoePrefab;
    public float attackRate;
    private float nextAttackTime = 3;

    new void Update()
    {
        base.Update();
        if (Time.time >= nextAttackTime)
        {
            PerformAOEAttack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void PerformAOEAttack()
    {
        // Instantiate the AOE effect at the Warlord's position
        GameObject aoeEffect = Instantiate(aoePrefab, transform.position, Quaternion.identity);

        // Set the AOE effect parameters if needed (e.g., damage, radius)
        NewBehaviourScript aoeScript = aoeEffect.GetComponent<NewBehaviourScript>();
        if (aoeScript != null)
        {
            aoeScript.SetDamage(10); // Example damage value
            aoeScript.SetRadius(5f); // Example radius value
        }

        Destroy(aoeEffect, 2f); // Destroy the AOE effect after 2 seconds
    }
}
