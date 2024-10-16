using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;   // Speed of the projectile
    public float lifetime = 10f;  // Time after which the projectile will be destroyed automatically
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
        transform.Translate(speed * Time.deltaTime * direction);  // For 2D, use Vector2.up
    }

    // Destroy the projectile when it collides with something
    void OnTriggerEnter2D(Collider2D other)
    {
        // Add logic for collision (e.g., destroy the projectile or deal damage)
        Destroy(gameObject);  // Destroy the projectile upon collision
    }
}
