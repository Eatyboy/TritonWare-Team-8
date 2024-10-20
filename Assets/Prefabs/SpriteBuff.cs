using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBuff : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    public Sprite sprite1;
    public Sprite sprite2;

    private float spriteSwitchInterval = 0.5f; // Time between switching sprites
    private bool isUsingSprite1 = true; // Tracks which sprite is currently active

    private float nextSpriteSwitchTime = 0f; // Tracks when to switch to the next sprite

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    protected void Update()
    {
        AlternateSprites();

    }

    protected void AlternateSprites()
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
}
