using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    // Counts the number of enemy collisions
    private int collisionCount = 0; 
    private PlayerCheckpoint checkpoint;

    void Start()
    {
        checkpoint = GetComponent<PlayerCheckpoint>();
        if (checkpoint == null)
        {
            Debug.LogError("PlayerCheckpoint script not found on the player.");
        }
    }

    public void HandleEnemyCollision()
    {
        collisionCount++;
        Debug.Log($"Player hit by enemy! Collision count: {collisionCount}");

        if (collisionCount < 3)
        {
            if (checkpoint != null)
            {
                // Respawn player at the last checkpoint
                checkpoint.Respawn();
                Debug.Log("Player respawned after being hit.");
            }
            else
            {
                Debug.LogWarning("Checkpoint reference is null, cannot respawn.");
            }
        }
        else
        {
            Debug.Log("Game over condition met. Switching to game over scene.");
            // Replace "GameOver" with the actual name of your game over scene
            SceneManager.LoadScene("GameOver"); 
        }
    }



    ////    [SerializeField] private int playerHealth = 0;
    ////    private PlayerCheckpoint checkpoint;

    ////    // Start is called before the first frame update
    ////    void Start()
    ////    {
    ////        checkpoint = GetComponent<PlayerCheckpoint>();
    ////        //if (checkpoint != null )
    ////        //{
    ////        //    Debug.LogError("PlayerCheckpoint Script not found on the player");
    ////        //}
    ////    }
    //------------------------------------OLD LOGIC-------------------------
    //public void TakeDamage(int damage)
    //{
    //    playerHealth -= damage;
    //    Debug.Log($"Player took damage! current health: {playerHealth}");

    //    if (playerHealth < 0 )
    //    {
    //        Respawn();
    //    }
    //}

    //private void Respawn()
    //{
    //    if( checkpoint != null )
    //    {
    //        checkpoint.Respawn();
    //        playerHealth = 0; //Reset the health after respawn

    //        Debug.Log("Player has died and repawned. Health reset to 3.");
    //    }
    //}
    //------------------------------------OLD LOGIC-------------------------

    //public void HandleEnemyCollision()
    //{
    //   playerHealth++;
    //    Debug.Log($"Player hit by the enemy! Playerhealth :{playerHealth}");

    //    if(playerHealth <3 )
    //    {
    //        if(checkpoint != null)
    //        {
    //            checkpoint.Respawn();

    //        }
    //    }
    //    else
    //    {
    //        // gameover condition is met. switching to the game over scene
    //        SceneManager.LoadScene("GameOver");
    //    }
    //}
}
