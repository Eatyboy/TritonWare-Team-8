using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMovement : MonoBehaviour
{
    public Transform player;         // Reference to the player transform
    public float radius;        // Distance from the player
    public float heightAmplitude; // Amplitude of vertical movement
    public int numberOfSquares;  // Number of squares to spawn
    public float rotationSpeed;
    public float squareIndex;

    public void Init(Transform player, float radius, float heightAmplitude, int numberOfSquares, float rotationSpeed, float squareIndex)
    {
        this.player = player;
        this.radius = radius;
        this.heightAmplitude = heightAmplitude;
        this.numberOfSquares = numberOfSquares;
        this.rotationSpeed = rotationSpeed;
        this.squareIndex = squareIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSquares();
    }

    private void MoveSquares()
    {

        float time = Time.time; // Get the current time

       
        float angle = time * rotationSpeed * Mathf.Deg2Rad + (squareIndex * Mathf.PI * 2 / numberOfSquares);
        // Calculate position based on angle, with vertical movement included
        Vector3 position = player.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * heightAmplitude, 0);
        transform.position = position; // Update square position
    }
}