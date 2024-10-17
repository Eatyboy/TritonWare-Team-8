using UnityEngine;

public class EProjectile : MonoBehaviour
{
    public float speed = 8f;   // Speed of the projectile
    public float lifetime = 10f;  // Time after which the projectile will be destroyed automatically
    public int dmg = 2;  // Damage of the projectile
    private Vector2 direction;

    void Start()
    {
        // Destroy the projectile after `lifetime` seconds
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Normalize the direction vector
    }

    void Update()
    {
        // Move the projectile forward in a straight line (in the direction it is facing)
        transform.Translate(Vector3.up * speed * Time.deltaTime);  // For 2D, use Vector2.up
    }

    // Destroy the projectile when it collides with something
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the enemy
            collision.GetComponent<Player>().TakeDamage(dmg);

            // Destroy the projectile after dealing damage
            Destroy(gameObject);
        }
    }
}
