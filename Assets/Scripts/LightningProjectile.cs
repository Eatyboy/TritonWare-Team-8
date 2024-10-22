using System.Collections;
using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    public float speed;
    private Collider2D currentTarget;
    public int chainsLeft;
    private float chainRange;
    public float damage;

    public void Initialize(Collider2D target, float dmg, int maxChains, float range)
    {
        currentTarget = target;
        damage = dmg;
        chainsLeft = maxChains;
        chainRange = range;

        StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget() // No generic <T> here
    {
        while (currentTarget != null)
        {
            // Move towards the current target
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Check if we reached the target
            if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.1f)
            {
                HitTarget(currentTarget);
            }

            yield return null; // Wait for the next frame
        }

        Destroy(gameObject); // Destroy after reaching the final target
    }

    private void HitTarget(Collider2D target)
    {
        target.GetComponent<IEnemy>().TakeDamage((int)damage);
        DamagePopupManager.Instance.NewPopup(damage, target.transform.position);

        if (chainsLeft > 0)
        {
            Collider2D nextTarget = FindNextTarget();
            if (nextTarget != null)
            {
                currentTarget = nextTarget;
                chainsLeft--;
            }
            else
            {
                Destroy(gameObject); // No more targets, destroy the projectile
            }
        }
        else
        {
            Destroy(gameObject); // Max chains reached, destroy the projectile
        }
    }

    private Collider2D FindNextTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, chainRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit != currentTarget)
            {
                return hit;
            }
        }
        return null;
    }
}
