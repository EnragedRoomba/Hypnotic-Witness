using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private CharacterController characterController;
    private InputAction moveInput;
    private InputAction runInput;
    private InputAction jumpInput;

    //------------------------------------------------------------
    private float currentMoveSpeed = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float lookangle = 0.0f;

    //-----------------------------------------------------------

    //Character 

    [SerializeField] private float walkSpeed = 5.5f;
    [SerializeField] private float runningSpeed = 9.0f;
    [SerializeField] private float jumpStrength = 2.5f;
    [SerializeField] private float gravity = 20.5f;

    private bool jumped = false;


    //Camera


    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float angleLimit = 90f;



    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();

        characterController = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");
        runInput = InputSystem.actions.FindAction("Sprint");
        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jumped;



        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentMoveSpeed = walkSpeed;

    }

    private void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        if (!characterController.isGrounded)
        {
            jumped = false;
        }

        currentMoveSpeed = runInput.IsPressed() ? walkSpeed : walkSpeed;
        HandleMovement(moveVector);
        HandleLooking(mouseDelta);

    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(moveVector.y * currentMoveSpeed, moveVector.x * currentMoveSpeed);

        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);
        if (jumped && characterController.isGrounded)
        {
            moveDirection.y = jumpStrength;
        }
        else 
        {
            moveDirection.y = oldY;
        
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        characterController.Move(moveDirection* Time.deltaTime);

    }

    private void Jumped(InputAction.CallbackContext context)
    {
        jumped = true;

    }


    private void HandleLooking(Vector2 mouseDelta)
    {
        lookangle += -mouseDelta.y * sensitivity;
        lookangle = Mathf.Clamp(lookangle, -angleLimit, angleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookangle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * sensitivity, 0);

    }

}
