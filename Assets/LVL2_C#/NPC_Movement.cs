using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    
    public Transform pointA; // The starting point
    public Transform pointB; // The destination point
    public float speed = 2f; // Speed of the NPC
    private Vector3 targetPoint; // Current target point

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPoint = pointB.position; // Initial target is point B
        animator.SetBool("isWalking", true); // Trigger walk animation
    }

    void Update()
    {
        // Move the NPC towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        
        // Check if the NPC has reached the target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            // Switch target point
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
            
            // Rotate the NPC to face the next target point
            Vector3 direction = (targetPoint - transform.position).normalized;
            transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));
        }
    }
}


