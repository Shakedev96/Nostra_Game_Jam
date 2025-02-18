using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHealth component and call HandleEnemyCollision()
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.HandleEnemyCollision();
            }
            else
            {
                Debug.LogWarning("PlayerHealth script not found on the player.");
            }
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
        
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();   

    //         if (playerHealth != null)
    //        {
    //            //reduces the players health by 1
    //            playerHealth.HandleEnemyCollision();
    //        }
    //    }
    //}


}
