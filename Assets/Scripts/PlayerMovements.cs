using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;

    float RotationX;
    float RotationY;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

   
    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        RotationY += mouseX;
        RotationX -= mouseY;

        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        // rotate camera and orientation
        transform.rotation = Quaternion.Euler(RotationX, RotationY, 0);
        orientation.rotation = Quaternion.Euler(0, RotationY, 0);

    }

    
    
}
