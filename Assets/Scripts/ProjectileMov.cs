using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;   // Speed of the projectile
    public float lifetime = 10f;  // Time after which the projectile will be destroyed automatically
    public int dmg;
    [SerializeField] bool enemy = false;
    private Vector2 direction;
    private Player player;

    void Start()
    {
        // Destroy the projectile after `lifetime` seconds
        Destroy(gameObject, lifetime);
        player = FindObjectOfType<Player>();
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemy)
        {
            // Do dmg modification here
            // Deal damage to the enemy
            int damage = (int)(dmg * player.damage);
            collision.GetComponent<Enemy>().TakeDamage(damage);

            DamagePopupManager.Instance.NewPopup(damage, collision.transform.position);

            // Destroy the projectile after dealing damage
            Destroy(gameObject);
        }else if (collision.CompareTag("Player") && enemy)
        {
            // Deal damage to the enemy
            collision.GetComponent<Player>().TakeDamage(dmg);

            // Destroy the projectile after dealing damage
            Destroy(gameObject);
        }
    }

}
