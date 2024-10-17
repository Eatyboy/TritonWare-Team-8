using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float duration; 
    private Vector2 direction;
    private float length; // Length of the beam

    public void Fire(Vector2 dir, float beamLength, float beamDuration)
    {
        direction = dir.normalized;
        length = beamLength;
        duration = beamDuration;

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
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;

        // Destroy the beam after the duration
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(5);
        }
    }
}
