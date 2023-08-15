using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Ckeck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float modificadorGravedad;

    [Header("Stamina")]
    public float maxStamina;
    public float staminaDrain;
    public float staminaRegen;
    public float stamina;
    private bool tired;
    private bool Running;

    [Header("Crouch")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYscale;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Physics.gravity *= modificadorGravedad;

        startYscale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina >= -1)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !tired)
            {
                Running = true;
                moveSpeed = 6;
            }

            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                moveSpeed = crouchSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
                    moveSpeed = 4;
                }

            else if (Input.GetKeyUp(KeyCode.LeftShift)) 
            {
                moveSpeed = 4;
                Running = false;
            }

            if(!Running || tired)
            {
                if (stamina <= maxStamina)
                {
                    stamina += staminaRegen * Time.deltaTime;
                }
            }
            if (Running)
            {
                stamina -= staminaDrain * Time.deltaTime;
            }
        }
               

            if(stamina <= 0)
            {
                tired = true;
            }

            if (stamina >= 100) { 
                tired = false;
        }
       

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag; 
        }
        else
        {
            rb.drag = 0;
        }

        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if(grounded)
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
                readyToJump = false;

                Jump();

                Invoke(nameof(resetJump), jumpCoolDown);
        }
 
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (grounded == false)
        { 
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);


        }
    }

    private void SpeedControl()
    {
        Vector3 limitVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity is needed

    if(limitVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = limitVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //recetear la velocidad de y
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);   
    }

    private void resetJump()
    {
        readyToJump = true;
    }
}