using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveA : MonoBehaviour
{

    
    private Rigidbody playerRB;

    private Animator anim;

    [Header("Player Settings")]
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    [SerializeField] private float walkSpeed, runSpeed, currentSpeed, idleSpeed = 0f;

    [Header("Jump Settings")]
    [SerializeField] float refFloat;
    [SerializeField] bool isGrounded;
    [SerializeField] float gravityMod;

    [SerializeField] private float jumpForce;

    private const string _groundLayer = "Ground";
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = walkSpeed;

        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAnimController();
        PlayerRun();
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
        }

    }

    void PlayerRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
           
            Debug.Log("isRunning");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            Debug.Log("isWalking");
        }
    }

    void PlayerAnimController()
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetFloat("Speed_f", currentSpeed);
        }

        else if(horizontalInput == 0 && verticalInput == 0)
        {
            anim.SetFloat("Speed_f", idleSpeed);
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
        {
            isGrounded = true;
        }
    }
}
