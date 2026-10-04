using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;


    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }


    private void MovePlayer()
    {
        //Calculate movement direction

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);



    }




















}
