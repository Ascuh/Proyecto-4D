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

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Transform orientation;
    [SerializeField] CapsuleCollider playerCollider;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float modificadorGravedad;

    public float maxStamina;
    public float staminaDrain;
    public float staminaRegen;
    public float stamina;
    private bool tired;
    private bool Running;
    bool escaleras;

    public float crouchSpeed;
    public float crouchYScale;
    private float startYscale;

    public Monstruo2 mon2;

    public bool life = true;

    public AudioSource respirar;

    [Header("Camera Bobbing")]
    public Transform cameraTransform; // Referencia a la cámara
    public float bobFrequency = 3f;   // Frecuencia del movimiento
    public float bobAmplitude = 0.05f; // Amplitud del movimiento (qué tanto se mueve)
    private float defaultCameraY;      // Para recordar la posición original de la cámara
    private float bobbingTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la referencia del Rigidbody y la gravedad
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Physics.gravity *= modificadorGravedad;

        startYscale = transform.localScale.y;

        // Posición original de la cámara
        defaultCameraY = cameraTransform.localPosition.y;

        // Reinicia la gravedad al valor inicial
        Physics.gravity = new Vector3(0, -9.81f * modificadorGravedad, 0);
    }


    // Update is called once per frame
    void Update()
    {
        // Control de bobbing de la cámara basado en si está corriendo o caminando
        if (Running)
        {
            bobFrequency = 15f;  // Frecuencia para correr
            bobAmplitude = 0.05f;  // Amplitud para correr
        }
        else
        {
            bobFrequency = 10f;  // Frecuencia para caminar
            bobAmplitude = 0.03f;  // Amplitud para caminar
        }

        // Hacer un Vector para el shader, usado para el movimiento del pasto respecto a la posición del jugador
        Shader.SetGlobalVector("_Player", transform.position + Vector3.up * playerCollider.radius);

        // Movimiento de la cámara
        if (grounded && (horizontalInput != 0 || verticalInput != 0))
        {
            bobbingTimer += Time.deltaTime * bobFrequency;
            float bobbingOffset = Mathf.Sin(bobbingTimer) * bobAmplitude;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, defaultCameraY + bobbingOffset, cameraTransform.localPosition.z);
        }
        else
        {
            // Reinicia la posición de la cámara cuando el jugador se detiene
            bobbingTimer = 0;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, defaultCameraY, cameraTransform.localPosition.z);
        }

        if (RaycastCam.lastimado)
            moveSpeed = 2;
        else
        {
            if (stamina >= -1)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && !tired)
                {
                    Running = true;
                    moveSpeed = 6;
                }
                else if (Input.GetKeyDown(KeyCode.LeftControl) && !Running)
                {
                    transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                    rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                    moveSpeed = crouchSpeed;
                }
                else if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
                    if (escaleras)
                        moveSpeed = 4;
                    else
                        moveSpeed = 3;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    moveSpeed = 3;
                    Running = false;
                }
            }

            if (!Running || tired)
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

            if (stamina <= 0)
            {
                tired = true;
                respirar.enabled = true;
                moveSpeed = 3;
            }

            if (stamina >= 100)
            {
                respirar.enabled = false;
                tired = false;
            }
        }

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Handle drag
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
        if (grounded)
            MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(resetJump), jumpCoolDown);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On slope
        if (OnSlope())
        {
            Vector3 slopeDirection = GetSlopeMoveDirection() * moveSpeed * 20f;
            slopeDirection.y = Mathf.Clamp(slopeDirection.y, -10f, 0f);  // Limitar el movimiento vertical hacia abajo
            rb.AddForce(slopeDirection, ForceMode.Force);
        }
        else if (grounded)
        {
            // On ground
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            // In air
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // Mantén la gravedad activada en todo momento
        rb.useGravity = true;
    }

    private void SpeedControl()
    {
        Vector3 limitVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity if needed
        if (limitVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = limitVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Resetear la velocidad en Y
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("deny"))
        {
            mon2.deny = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("deny"))
        {
            Invoke("DenyFalse", 10);
        }
    }

    private void DenyFalse()
    {
        mon2.deny = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public void die()
    {
        Destroy(gameObject);
    }

    private void OnColissionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("escaleras"))
        {
            escaleras = true;
        }
        else
        {
            escaleras = false;
        }
    }
}
