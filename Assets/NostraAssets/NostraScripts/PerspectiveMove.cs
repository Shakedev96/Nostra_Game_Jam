using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveMove : MonoBehaviour
{
    
    private Rigidbody playerRB;

    //

    [Header("Player Settings")]
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    [SerializeField] private float runSpeed, currentSpeed;
    [SerializeField] public bool isDash, jumping , shooting;

    [SerializeField] SoundSys soundSys;
    


    [Header("Jump Settings")]
    [SerializeField] float refFloat;
    [SerializeField] bool isGrounded;
    [SerializeField] float gravityMod;
    [SerializeField] private float jumpForce;
    private const string _groundLayer = "Ground";

    [Header("Player Animation Settings")]
    private Animator anim;
    int isRunningHash;

    int isJumpingHash;
    int isDashingHash;
    int isShootingHash;
    int isSquatingHash;
    


    void Awake()
    {
        isRunningHash = Animator.StringToHash("isRun");
        isJumpingHash = Animator.StringToHash("isJump");
        isDashingHash = Animator.StringToHash("isDash");
        isShootingHash = Animator.StringToHash("isShooting");
        isSquatingHash = Animator.StringToHash("isSquat");

    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        currentSpeed = runSpeed;
        soundSys = FindObjectOfType<SoundSys>();
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAnimController();
        PlayerDash();
        Jump();
    }
    void FixedUpdate()
    {
        
    }

    void PlayerMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 _inputkey = new Vector3(horizontalInput , 0 ,verticalInput );

        //playerRB.velocity = _inputkey * walkSpeed;

        playerRB.MovePosition(transform.position + _inputkey * currentSpeed * Time.deltaTime);

        
        

        if(_inputkey.magnitude >= 0.1f)
        {
            float rotationAngle = Mathf.Atan2(_inputkey.x,  _inputkey.z) * Mathf.Rad2Deg;
            float smoothRoation = Mathf.SmoothDampAngle(transform.eulerAngles.y , rotationAngle ,ref refFloat , 0.1f);

            transform.rotation = Quaternion.Euler(0 ,smoothRoation ,0);
        }

        


    }
    

    void Jump()
    {  
        
        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumping = true;
            if(jumping)
            {
                anim.SetBool(isJumpingHash,true);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }

    }

    void PlayerDash()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 2.5f * runSpeed;
            isDash = true;

            //soundSys.PlayerDashSound();
            /* if(isDashing)
            {
                playerRB.AddRelativeForce(Vector3.forward * runSpeed, ForceMode.Impulse);
            } */
           
            Debug.Log("isDashing");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDash = false;
            currentSpeed = runSpeed;
            Debug.Log("isRunning");
        }
    }

    void Shoot()
    {
        
    }

    

    void PlayerAnimController()
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool(isRunningHash,true);
        }

        else if(horizontalInput == 0 && verticalInput == 0)
        {
            anim.SetBool(isRunningHash,false);
        }

        // for jumping
        if(jumping == true)
        {
            anim.SetBool(isJumpingHash,true);
        }
        else if(jumping == false)
        {
            anim.SetBool(isJumpingHash,false);
        }

        //for dash
        if(isDash == true)
        {
            anim.SetBool(isDashingHash,true);
        }
        else if(isDash == false)
        {
            anim.SetBool(isDashingHash,false);
        }
        // for shooting
        if(shooting == true)
        {
            anim.SetBool(isShootingHash,true);
        }
        else if(shooting == false)
        {
            anim.SetBool(isShootingHash,false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
        {
            isGrounded = true;
            jumping = false;
        }
    }


}
