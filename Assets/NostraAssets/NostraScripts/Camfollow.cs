using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfollow : MonoBehaviour
{
    
    [Header("Target Settings")]
    [SerializeField] private Transform playerTransform;  // The player's transform to follow
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);  // Offset position of the camera relative to the player

    [Header("Follow Smoothness")]
    [SerializeField, Range(0f, 1f)] private float smoothSpeed = 0.125f;  // Controls how smooth the camera movement is

    [Header("Boundary Settings")]
    [SerializeField] private bool useBoundaries = false;  // Enable if you want the camera to stay within a certain boundary
    [SerializeField] private Vector3 minBoundary;  // Minimum boundary for camera movement (x, y, z)
    [SerializeField] private Vector3 maxBoundary;  // Maximum boundary for camera movement (x, y, z)

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Calculate the desired position
        Vector3 desiredPosition = playerTransform.position + offset;

        // Smoothly transition to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply boundaries if enabled
        if (useBoundaries)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBoundary.x, maxBoundary.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBoundary.y, maxBoundary.y);
            smoothedPosition.z = Mathf.Clamp(smoothedPosition.z, minBoundary.z, maxBoundary.z);
        }

        // Update camera position
        transform.position = smoothedPosition;

        // Keep the camera looking at the player, or you can set a fixed forward direction if preferred
        transform.LookAt(playerTransform);
    }
}


