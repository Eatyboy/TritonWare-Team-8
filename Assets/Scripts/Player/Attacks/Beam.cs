using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float duration;         // How long the beam lasts
    private Vector2 direction;      // Direction the beam will fire
    private float length;           // Length of the beam
    public LineRenderer lineRenderer; // LineRenderer component for visualizing the beam
    public float rotationSpeed = 30f; // Speed of rotation (degrees per second)

    private Transform pivot; // Pivot point around which the beam rotates

    public void Fire(Vector2 dir, float beamLength, float beamDuration, Transform pivotPoint = null)
    {
        direction = dir.normalized;
        length = beamLength;
        duration = beamDuration;
        pivot = pivotPoint; // The pivot point to rotate around

        // Calculate the end position of the beam
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * length);

        // Draw the beam as a line
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Start the rotation if a pivot point is provided
        if (pivot != null)
        {
            StartCoroutine(RotateBeam());
        }

        // Set the beam to destroy after its duration
        StartCoroutine(DisableBeam());
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
            collision.GetComponent<Player>().TakeDamage(5);
        }
    }
}
