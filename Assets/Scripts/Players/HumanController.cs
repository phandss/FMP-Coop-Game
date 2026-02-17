using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float sprintMultiplyer = 2;

    [SerializeField] private CharacterController controller;
    [SerializeField]private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 velocity;

    private void Awake()
    {
        controller = GetComponentInChildren<CharacterController>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        
        
    }
}
