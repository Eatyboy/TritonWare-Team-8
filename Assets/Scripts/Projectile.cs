using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;        // Speed of the projectile
    public float lifetime = 10f;     // Time after which the projectile will be destroyed automatically
    public int dmg;                  // Damage value
    [SerializeField] private bool isEnemyProjectile = false; // True if fired by an enemy
    private Vector2 direction;
    private Transform target;        // Target for homing projectiles
    private bool isHoming = false;   // Determines if the projectile is homing
    private Player player;           // Reference to the player

    void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
        player = FindObjectOfType<Player>();

        if (isHoming && target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
    }

    void Update()
    {
        if (isHoming && target != null)
        {
            // Adjust direction towards target for homing projectiles
            direction = (target.position - transform.position).normalized;
        }

        // Move the projectile in the given direction
        transform.Translate(speed * Time.deltaTime * direction);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void SetHomingTarget(Transform targetTransform)
    {
        isHoming = true;
        target = targetTransform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnemyProjectile)
        {
            // Enemy projectile hits the player
            collision.GetComponent<Player>().TakeDamage(dmg);
            Destroy(gameObject); // Destroy the projectile after dealing damage
        }
        else if (collision.CompareTag("Enemy") && !isEnemyProjectile)
        {
            // Player projectile hits the enemy
            collision.GetComponent<IEnemy>().TakeDamage(dmg);
            DamagePopupManager.Instance.NewPopup(dmg, collision.transform.position);
            Destroy(gameObject); // Destroy the projectile after dealing damage
        }
        else if (collision.CompareTag("Obstacle") && isEnemyProjectile)
        {
            // Destroy the projectile if it hits an obstacle
            Destroy(gameObject);
        }else if(collision.CompareTag("Shield") && isEnemyProjectile)
        {
            ReflectProjectile(collision);
        }
    }

    private void ReflectProjectile(Collider2D shield)
    {
        Debug.Log("before: " + direction);
        // Calculate the normal vector of the shield (its "up" direction)
        Vector2 shieldNormal = shield.transform.up;
        Debug.Log("after: " + direction);


        // Reflect the current direction using the shield's normal
        direction = Vector2.Reflect(direction, shieldNormal).normalized;

        // Rotate the projectile to match the new reflected direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

}
