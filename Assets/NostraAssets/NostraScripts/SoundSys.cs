using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSys : MonoBehaviour
{
    [Header("Player Audio")]

    public AudioSource playerSource;
    public AudioClip playerJump;
    public AudioClip playerDash;

    //public AudioClip playerShoot;
    public PlayerCon platPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerSource = FindObjectOfType<AudioSource>();
        //platPlayer = GameObject.Find("PlayerPlatformer").GetComponent<PlayerCon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerJumpSound()
    {
        
        
            playerSource.PlayOneShot(playerJump,1f);
        
        
    }
    public void PlayerDashSound()
    {
        playerSource.PlayOneShot(playerDash,1f);
    }
}
