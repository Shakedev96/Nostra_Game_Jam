using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMec : MonoBehaviour
{
    //private bool isTouched = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // isTouched = true;
            PlayerCheckpoint playerCheckpoint = collision.gameObject.GetComponent<PlayerCheckpoint>();
            if (playerCheckpoint != null)
            {
                playerCheckpoint.Respawn();
            }
            else
            {
                Debug.LogWarning("player checkpoint script is not found on the player");
            }
        }

    }

}
