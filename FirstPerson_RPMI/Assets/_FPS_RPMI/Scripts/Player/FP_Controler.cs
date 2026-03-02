using UnityEngine;
using UnityEngine.InputSystem;

public class FP_Controler : MonoBehaviour
{
    #region general variables
    [Header("movement & Look")]
    [SerializeField] GameObject camHolder;
    [SerializeField] float Speed = 5f;
    [SerializeField] float sSpeed = 8f;
    [SerializeField] float cSpeed = 3f;
    [SerializeField] float maxForce = 1; //FuerzaMaxima de aceleracion
    [SerializeField] float sensitivity = 0.1f; //Sensibilidad base del raton

    [Header("Jump & GroundCheck")]
    [SerializeField] bool isGrounded;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;


    [Header("player state bools")]
    [SerializeField] bool sprinting;
    [SerializeField] bool Crouching;

    //Variables de autoreferencia
    Rigidbody rb;
    Animator anim;

    //Variables de Input
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //Lock del cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

    }

    // Update is called once per frame
    void Update()
    {
        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void LateUpdate()
    {
        Cameralook();
    }

    void Cameralook()
    {
        //rotacion del personaje (horizontal)
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
        //rotacion de la camara (vertical)
        lookRotation += (-lookInput.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);
    }

    void Movement()
    {
        //Definir los dos vectores que permiten la aceleracion
        Vector3 CurrentVelocity = rb.linearVelocity;
        Vector3 TargetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
        //A la direccion a alcanzar le multiplicamos la velocidad
        TargetVelocity *= Crouching ? cSpeed : sprinting ? sSpeed : Speed;
        //Convertir la direccion al eje mundial (Local->World)
        TargetVelocity = transform.TransformDirection(TargetVelocity);
        //Calcular el cambio de velocidad (aceleracion)
        Vector3 velocityChange = (TargetVelocity - CurrentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        //aplicacion del movimiento (Direccion + aceleracion)
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up *jumpForce, ForceMode.Impulse);
        }
    }

    #region Input methots
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && !Crouching)
        {
            sprinting = true;
        }
        if (context.canceled)
        {
            sprinting = false;
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Crouching = !Crouching;
            anim.SetBool("Crouching", Crouching);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    #endregion
}
