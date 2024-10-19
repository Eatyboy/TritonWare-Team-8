using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float duration;         // How long the beam lasts
    private Vector2 direction;      // Direction the beam will fire
    private float length;           // Length of the beam
    public float dmg;
    public LineRenderer lineRenderer; // LineRenderer component for visualizing the beam
    public float rotationSpeed = 30f; // Speed of rotation (degrees per second)
    private BoxCollider2D boxCollider; // BoxCollider2D for collision detection

    private Transform pivot; // Pivot point around which the beam rotates

    public Sprite sprite1;
    public Sprite sprite2;

    private float spriteSwitchInterval = 0.5f; // Time between switching sprites
    private bool isUsingSprite1 = true; // Tracks which sprite is currently active
    protected SpriteRenderer spriteRenderer;

    private float nextSpriteSwitchTime = 0f; // Tracks when to switch to the next sprite

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire(Vector2 dir, float beamLength, float beamDuration, Transform pivotPoint = null)
    {
        direction = dir.normalized;
        length = beamLength;
        duration = beamDuration;
        pivot = pivotPoint; // The pivot point to rotate around

        // Initialize the LineRenderer if not already assigned
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set the beam properties for LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * length);

        // Set initial positions for the beam
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Add and configure the BoxCollider2D
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true; // Set the collider as a trigger
        UpdateCollider(startPosition, endPosition);

        // Start the rotation if a pivot point is provided
        if (pivot != null)
        {
            StartCoroutine(RotateBeam());
        }

        // Set the beam to destroy after its duration
        StartCoroutine(DisableBeam());
    }

    private void Update()
    {
        AlternateSprites();

    }

    private void UpdateCollider(Vector2 startPosition, Vector2 endPosition)
    {
        // Calculate the center and size of the box collider to match the beam's visual line
        Vector2 center = (startPosition + endPosition) / 2; // Midpoint between start and end positions
        float beamLength = Vector2.Distance(startPosition, endPosition);
        boxCollider.offset = transform.InverseTransformPoint(center);
        boxCollider.size = new Vector2(beamLength, lineRenderer.startWidth); // Length and width of the beam
    }

    private IEnumerator RotateBeam()
    {
        while (true)
        {
            // Rotate the beam around the pivot point
            transform.RotateAround(pivot.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            UpdateBeamPositions();
            yield return null;
        }
    }

    private void UpdateBeamPositions()
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * length);
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        // Update the collider to match the new positions after rotation
        UpdateCollider(startPosition, endPosition);
    }

    private IEnumerator DisableBeam()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject); // Destroy the beam object after the duration expires
    }

    // Handle collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            collision.GetComponent<Player>().TakeDamage((int)dmg);
        }
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
