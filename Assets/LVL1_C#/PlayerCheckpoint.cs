using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private PlayerCon playerSpwan; 
    private Vector3 lastCheckpointPosition; // Position of the last collected checkpoint

    private void Start()
    {
        // Initialize the last checkpoint to the player's starting position
        lastCheckpointPosition = transform.position;
        playerSpwan = GetComponent<PlayerCon>();
        playerSpwan.stamina = 100;

    }

    public void Respawn()
    {
        // Respawn the player at the last checkpoint position
        Debug.Log("Respawning at the last checkpoint...");
        transform.position = lastCheckpointPosition;
        playerSpwan.stamina = 100;
        //playerSpwan.UpdateUI();

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a checkpoint
        if (other.CompareTag("CheckPoint"))
        {
            // Update the last checkpoint position to the current player position
            lastCheckpointPosition = transform.position;
            Debug.Log($"Checkpoint reached! Last position updated to: {lastCheckpointPosition}");

            // Optional: Deactivate the checkpoint after it's collected
            other.gameObject.SetActive(false);
        }
    }



    //private Vector3 lastCheckpointPosition; // Position of the last collected checkpoint

    //private void Start()
    //{
    //    // Initialize the last checkpoint to the player's starting position
    //    lastCheckpointPosition = transform.position;
    //}

    //private void Update()
    //{
    //    // Respawn logic, if needed, can be triggered by some external event
    //}

    //public void Respawn()
    //{
    //    // Respawn the player at the last checkpoint position
    //    Debug.Log("Respawning at the last checkpoint...");
    //    transform.position = lastCheckpointPosition;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Check if the player collided with a checkpoint
    //    if (other.CompareTag("CheckPoint"))
    //    {
    //        // Update the last checkpoint position to the player's current position
    //        lastCheckpointPosition = transform.position;
    //        Debug.Log($"Checkpoint reached! Last position updated to: {lastCheckpointPosition}");

    //        // Optional: Deactivate the checkpoint after it's collected
    //        other.gameObject.SetActive(false);
    //    }
    //}

}
