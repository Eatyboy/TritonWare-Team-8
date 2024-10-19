using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Enemy that moves and splits
public class Wanderer : IEnemy
{
    [SerializeField] private int numberOfSplits = 1;
    public float splitScaleFactor = 0.9f;
    private int maxHP;

    new void Start()
    {
        maxHP = health;
        base.Start();
    }

    protected override void Die()
    {
        if (numberOfSplits > 0)
        {
            SplitIntoSmallerEnemies();
        }

        base.Die();

    }

    private void SplitIntoSmallerEnemies()
    {
        for (int i = 0; i < 2; i++)  // Spawn two smaller versions
        {
            // Instantiate a clone of the current enemy (the same prefab)
            GameObject smallerEnemy = Instantiate(gameObject, transform.position, Quaternion.identity);

            // Adjust the scale of the smaller enemy
            smallerEnemy.transform.localScale = transform.localScale * splitScaleFactor; // Scale down the size

            // Get reference to the enemy script and modify its properties
            Wanderer enemyScript = smallerEnemy.GetComponent<Wanderer>();

            // Reduce health and other properties for the smaller version
            enemyScript.health = (int) Mathf.Max(1, (this.maxHP * splitScaleFactor) / 2); // Half the health, at least 1
            enemyScript.ms = this.ms * 1.2f;                    // Optionally, increase speed
            enemyScript.numberOfSplits = this.numberOfSplits - 1; // Decrease split count

            // Optional: randomize the direction slightly to avoid overlap
            Rigidbody2D rb = smallerEnemy.GetComponent<Rigidbody2D>();
            Vector2 randomDirection = new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)).normalized;
            rb.AddForce(randomDirection * 2f, ForceMode2D.Impulse);  // Adjust force as needed
        }
    }
}
