using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject sentinelPrefab;
    public GameObject wandererPrefab;
    public GameObject homingProjectilePrefab;
    public GameObject beamPrefab;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // Alternate between spawning enemies and attacking
            if (Random.value > 0.5f)
            {
                SpawnEnemies();
            }
            else
            {
                PerformAttack();
            }

            nextAttackTime = Time.time + attackRate;
        }
    }

    private void SpawnEnemies()
    {
        // Spawn Sentinel
        Instantiate(sentinelPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        // Spawn Wanderer
        Instantiate(wandererPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private void PerformAttack()
    {
        if (Random.value > 0.5f)
        {
            // Fire homing projectiles
            ShootHomingProjectiles();
        }
        else
        {
            // Fire a beam
            FireBeam();
        }
    }

    private void ShootHomingProjectiles()
    {
        GameObject homingProjectile = Instantiate(homingProjectilePrefab, transform.position, Quaternion.identity);
        homingProjectile.GetComponent<HomingProjectile>().SetTarget(player);
    }

    private void FireBeam()
    {
        GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        beam.GetComponent<Beam>().Fire(Vector2.down, 5f, 1f);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * 10f) + (Vector2)player.position;
        return spawnPosition;
    }
}
