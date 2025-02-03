using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
   [Header("Player Settings")]
    [SerializeField] private Transform player;  // Reference to the player's transform
    [SerializeField] private Variables playerHealth;

    [Header("Radius Settings")]
    [SerializeField] private float followRadius = 10f;  // Radius within which the enemy starts following
    [SerializeField] private float attackRadius = 2f;   // Radius within which the enemy attacks

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;      // Speed at which the enemy follows the player
    [SerializeField] private float attackDamage = 10f;  // Damage dealt to the player on attack
    [SerializeField] private float dashMultiplier = 4f; // Multiplier for dash speed
    public bool follow;
    public float raycastDistance = 50f;  // Maximum distance for the raycast
    public LayerMask playerLayer;   // Layer mask to specify which objects are considered obstacles (e.g., walls)
    public bool showRaycastDebug = true; // Option to visualize the raycast in the scene view for debugging

    [Header("Attack Cooldown Settings")]
    [SerializeField] private float cooldownTime = 2f;   // Time in seconds between attacks
    private float lastAttackTime = -Mathf.Infinity;     // Tracks the time of the last attack

    private bool hasDashed = false; // To prevent continuous dashing while in Attack state
    private Rigidbody enemyRB;
    private Animator anim;

    private enum EnemyState { Idle, Follow, Attack }    // Possible states for the enemy
    private EnemyState currentState = EnemyState.Idle;

    void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Update the enemy's state based on distance
        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= followRadius)
        {
            currentState = EnemyState.Follow;
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        // Act based on the current state
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Follow:
                FollowPlayer();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
        }
    }

    private void Idle()
    {
        // Stop any movement
        enemyRB.velocity = Vector3.zero;
        follow = false;
        anim.SetBool("isFollow",false);
        anim.SetBool("bossDash",false);
    }

    private void FollowPlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        follow = true;
        anim.SetBool("isFollow",true);
        // Rotate to face the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    private void AttackPlayer()
    {
        // Check if the cooldown has passed since the last attack
        if (Time.time >= lastAttackTime + cooldownTime)
        {
            // Move towards the player at dash speed
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * dashMultiplier * Time.deltaTime;

            // Update the last attack time to the current time
            lastAttackTime = Time.time;

            // Optional: Visualize the ray in the editor for debugging
            if (showRaycastDebug)
            {
                Debug.DrawRay(transform.position, direction * raycastDistance, Color.green);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Check if the collision is with the player and if cooldown has passed
        if (other.gameObject.CompareTag("Player") && Time.time >= lastAttackTime + cooldownTime)
        {
            // Apply damage to the player on collision
            playerHealth.ApplyChange(-attackDamage);
            anim.SetBool("bossDash",true);
            
            Debug.Log("Enemy collided with player and dealt damage.");

            // Reset the last attack time after a successful attack
            lastAttackTime = Time.time;
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            anim.SetBool("BossDeath",true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw follow and attack radius in the editor for better visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
/*
{
    
    3 states
    1.idle 
    2.detect and follow player
    3. dash at player
     
    
}

    
  
     
    
 
 */
