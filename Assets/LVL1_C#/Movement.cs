using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Settings")]
    public float moveSpeed = 5f; // Speed of the player movement
    public float gravity = -9.81f; // Gravity force
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the player from tipping over
    }

    void Update()
    {
        // Get input for movement (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
       // float moveZ = Input.GetAxis("Vertical");

        // Move the player based on input
        Vector3 movement = new Vector3(moveX, 0,0) * moveSpeed;
        Vector3 currentVelocity = rb.velocity;

        // Apply movement while keeping the current Y velocity for gravity
        rb.velocity = new Vector3(movement.x, currentVelocity.y, 0);

        // Apply custom gravity if the player is not grounded
        if (!IsGrounded())
        {
            rb.velocity += Vector3.up * gravity * Time.deltaTime;
        }
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground using a raycast
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}


