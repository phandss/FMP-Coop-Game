using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovement : MonoBehaviour
{

    private Rigidbody rb;

    public float moveSpeed = 1;
    private float movementX;
    private float movementY;

    public float jumpForce;


    private void Start()
    {
        rb=GetComponent<Rigidbody>();
        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);
        rb.AddForce(movement*moveSpeed);
    }

    private void OnJump(InputValue jumpValue) 
    {
        
    }
}
