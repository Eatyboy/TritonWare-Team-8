using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy that can move in zig zag pattern and shoot burst projectiles
public class Phantom : Enemy
{
    public int burstCount = 3;  // Number of shots in a burst
    public float burstInterval = 0.2f; // Time between burst shots

    protected override void Update()
    {
        MoveInPattern();
        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(ShootBurst());
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void MoveInPattern()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * 2f;
        float y = Mathf.Cos(Time.time * moveSpeed) * 2f;
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }

    private IEnumerator ShootBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            AttackPlayer();
            yield return new WaitForSeconds(burstInterval);
        }
    }
}
