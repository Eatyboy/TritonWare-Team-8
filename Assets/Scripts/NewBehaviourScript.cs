using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float dmg;
    public float radius;

    public Sprite sprite1;
    public Sprite sprite2;

    private float spriteSwitchInterval = 0.5f; // Time between switching sprites
    private bool isUsingSprite1 = true; // Tracks which sprite is currently active
    protected SpriteRenderer spriteRenderer;
    private float nextSpriteSwitchTime = 0f;
    private Rigidbody2D rb;
    private Transform player;
    public bool isEnemyAttack;
    private Warlord warlord;


    private void Start()
    {
        warlord = GetComponent<Warlord>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AlternateSprites();

    }

    private void AlternateSprites()
    {
        if (Time.time >= nextSpriteSwitchTime)
        {
            // Toggle between sprite1 and sprite2
            if (isUsingSprite1)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }

            // Toggle the state
            isUsingSprite1 = !isUsingSprite1;

            // Set the next time to switch sprites
            nextSpriteSwitchTime = Time.time + spriteSwitchInterval;
        }
    }

    public void SetDamage(float value)
    {
        dmg = value;
    }

    public void SetRadius(float value)
    {
        radius = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Warlord>() != null)
        {
            return; // Exit the method, so we don't apply damage to the Warlord
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage((int)dmg);
            Debug.Log("Hit player");
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IEnemy>().TakeDamage((int)dmg);
        }

        // Optionally, draw the AOE radius in the editor for visualization
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
