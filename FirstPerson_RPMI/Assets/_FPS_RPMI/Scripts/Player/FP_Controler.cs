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
    [SerializeField] float sensibility = 0.1f; //Sensibilidad base del raton

    [Header("player state bools")]
    [SerializeField] bool sprinting;
    [SerializeField] bool Crouching;

    //Variables de autoreferencia
    Rigidbody rb;

    //Variables de Input
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
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
        
    }

    #region Input methots
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {

    }
    public void OnSprint(InputAction.CallbackContext context)
    {

    }
    public void OnCrouch(InputAction.CallbackContext context)
    {

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    #endregion
}
