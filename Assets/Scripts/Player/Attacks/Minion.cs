using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackCooldown;

    private Transform target; // The target enemy
    private float attackTimer;

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();

            // Check if the minion can attack
            if (Vector2.Distance(transform.position, target.position) <= attackRange && attackTimer <= 0)
            {
                Attack();
                attackTimer = attackCooldown;
            }
        }
        else
        {
            // Find the nearest enemy if no target
            FindTarget();
        }

        // Reduce attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    private void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f); // Adjust radius as needed
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                target = enemy.transform;
                break;
            }
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            IEnemy enemy = target.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damage);
                DamagePopupManager.Instance.NewPopup(damage, enemy.transform.position);
            }
        }
    }
}

