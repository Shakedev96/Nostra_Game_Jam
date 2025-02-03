using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStaticFollow : MonoBehaviour
{
       public Transform player; // Reference to the player's Transform
    public Vector3 offset;   // Offset distance between the player and the camera

    void LateUpdate()
    {
       // Smoothly move the camera to follow the player
    Vector3 desiredPosition = player.position + offset;
    transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f); // Adjust the 0.1f for different smoothing speeds
    }
    
}



