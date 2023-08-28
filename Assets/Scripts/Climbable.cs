using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour
{
    public LayerMask whatIsWall;
    public float wallCheckDistance = 0.5f;
    public float wallRunForce = 10f;
    public float wallClimbForce = 5f;

    private Rigidbody rb;
    private bool isWallRunning = false;
    private bool isWallClimbing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckForWall();
    }

    private void FixedUpdate()
    {
        if (isWallRunning)
        {
            // Apply wall running force
            rb.AddForce(transform.forward * wallRunForce, ForceMode.Force);
        }
        else if (isWallClimbing)
        {
            // Apply wall climbing force
            rb.AddForce(transform.up * wallClimbForce, ForceMode.Force);
        }
    }

    private void CheckForWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, wallCheckDistance, whatIsWall))
        {
            // Check if the wall is climbable
            if (CanClimb(hit))
            {
                isWallClimbing = true;
                isWallRunning = false;
            }
            // Check if the wall is suitable for wall running
            else if (CanWallRun(hit))
            {
                isWallRunning = true;
                isWallClimbing = false;
            }
            else
            {
                isWallRunning = false;
                isWallClimbing = false;
            }
        }
        else
        {
            isWallRunning = false;
            isWallClimbing = false;
        }
    }

    private bool CanClimb(RaycastHit hit)
    {
        // Implement your criteria for determining if the wall is climbable
        // For example, check the angle of the wall's normal vector
        return Vector3.Angle(hit.normal, Vector3.up) <= 60f;
    }

    private bool CanWallRun(RaycastHit hit)
    {
        // Implement your criteria for determining if the wall is suitable for wall running
        // For example, check the angle of the wall's normal vector and player's velocity direction
        return Vector3.Angle(hit.normal, Vector3.up) > 60f;
    }
}
