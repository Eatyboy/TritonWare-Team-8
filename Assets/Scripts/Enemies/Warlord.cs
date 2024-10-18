using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy that can shoot beam and projectile spread
public class Warlord : Enemy
{
    public GameObject beamPrefab;
    private bool attackTypeSwitch = true;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (attackTypeSwitch)
            {
                ShootRadialProjectiles();
            }
            else
            {
                FireBeam();
            }

            attackTypeSwitch = !attackTypeSwitch;
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void ShootRadialProjectiles()
    {
        int projectileCount = 8;
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            float angle = i * (360f / projectileCount); // Evenly spaced
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        }
    }

    private void FireBeam()
    {
        GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        beam.GetComponent<Beam>().Fire(Vector2.down, 5f, 1f); // Example beam parameters
    }
}
