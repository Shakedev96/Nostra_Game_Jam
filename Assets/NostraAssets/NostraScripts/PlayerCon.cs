using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    
    [Header("Player Settings")]
    
    [SerializeField] private float platformerSpeed = 5f; //idleSpeed = 0f;
     private AudioSource playerSource;
     [SerializeField] public ParticleSystem fart;
     //private AudioClip fartSound;
    private float horizontalMove;
    [SerializeField] float refFloat;
    [SerializeField] public Variables playerSpeed;
    [SerializeField] private Rigidbody playerRB;

    [SerializeField,Range(1f,15f)] private float rayDistance = 1.0f; // Distance the ray will travel


    [Header("Animation Stuff")]
    [SerializeField] private Animator anim;
    



    [Header("Player Fart Settings")]
    [SerializeField] private float fartForce;
    [SerializeField] public bool isFarting;
    [SerializeField, Range(0f, 2f)] private float gravityMod;
    [SerializeField] private float airControlFactor = 0.5f; // Control factor in the air for smoother movement
    [SerializeField] public bool isGrounded;
    [SerializeField] private const string ground = "Ground";
    [SerializeField] private int fartUsagePerJump;
    [SerializeField] private SoundSys soundSys;
    
    [Header("UI Bar Settings")]

    [SerializeField] private Image fartMeter;
    [SerializeField] public float stamina, maxStamina = 100f;

    [Header("Collectibles")]
    [SerializeField] public Collectibles pizza;
    private const string _food = "Food";
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
        anim = GetComponentInChildren<Animator>();
        soundSys = FindObjectOfType<SoundSys>();
    }

    // Update is called once per frame
    void Update()
    {
        PlatformerMove();
        FartJump();
        ImageBar();
        AnimationController();
        JumpRay();
    }

    void PlatformerMove()
{
    horizontalMove = Input.GetAxis("Horizontal");
    Vector3 hInput = new Vector3(horizontalMove, 0, 0);

    if (isGrounded)
    {
        // Full control on the ground
        playerRB.velocity = hInput * playerSpeed.Value;
    }
    else
    {
        // Limited control in the air
        playerRB.AddForce(hInput * platformerSpeed * airControlFactor, ForceMode.Force);
        
    }

    // Rotate player strictly to 0° (forward) or 180° (backward) along the Z-axis
    if (horizontalMove > 0)
    {
        // Face forward
        transform.rotation = Quaternion.Euler(0, 90, 0);
        
    }
    else if (horizontalMove < 0)
    {
        // Face backward
        transform.rotation = Quaternion.Euler(0, 270, 0);
    }
}



    void FartJump()
    {
        
        if (Input.GetKey(KeyCode.Space) && stamina > 0f)
        {
            isFarting = true;
            playerRB.AddForce(Vector3.up * fartForce, ForceMode.Acceleration);
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                soundSys.PlayerJumpSound();
                fartUsagePerJump++;
                fart.Play();
            }
            
        }
        else
        {
            isFarting = false;
            
        }
    }

    public void ImageBar()
    {
        if(isFarting)
        {
            stamina -= Mathf.Abs(fartUsagePerJump * Time.deltaTime);
            
            fartMeter.fillAmount = stamina/maxStamina;
            if(stamina < 0)
            {
                stamina = 0;
            }
        }

    }
    public void UpdateUI()
    {
        fartMeter.fillAmount = stamina/maxStamina;
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(ground))
        {
            isGrounded = true;
            fartUsagePerJump = 0;
        }
        
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ground))
        {
            isGrounded = false;
        }
    }

    

    public void AnimationController()
    {
        if(isFarting && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJump",true);
            
        }
        else if(isGrounded)
        {
            anim.SetBool("isJump",false);
            
        }
        if(horizontalMove > 0f || horizontalMove < 0f)
        {
            anim.SetBool("isRun",true);
        }
        else if(horizontalMove == 0f)
        {
            anim.SetBool("isRun",false);
        }
    }

    void JumpRay()
    {
        // Define the starting position and direction of the ray
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        // Create a layer mask for the "Enemy" layer
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int layerMask = 1 << enemyLayer;

        // Cast a ray downwards and check if it hits an object on the "Enemy" layer
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance, layerMask))
        {
            // Check if the hit object is on the "Enemy" layer
            if (hit.collider.gameObject.layer == enemyLayer)
            {
                // Destroy the hit enemy object
                Destroy(hit.collider.gameObject);
                Debug.Log("Enemy destroyed!");
            }
        }

        // Optional: Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

    }
}
