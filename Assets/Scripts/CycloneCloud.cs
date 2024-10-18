using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Enemy that can shoot beams that rotate around it's position
public class CycloneCloud : Enemy
{
    public GameObject beamPrefab;
    public float beamRotationSpeed = 30f;
    public float beamLength = 5f;
    public float beamDuration = 3f;

    protected override void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            ShootRotatingBeam();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void ShootRotatingBeam()
    {
        GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        Beam beamScript = beam.GetComponent<Beam>();
        beamScript.rotationSpeed = beamRotationSpeed; // Set the beam's rotation speed
        beamScript.Fire(Vector2.right, beamLength, beamDuration, transform); // Rotate around the Cyclone enemy
    }
}