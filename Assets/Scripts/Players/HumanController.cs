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
    private HumanInteraction _Interact;
    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponentInChildren<CharacterController>();
        rb = GetComponentInChildren<Rigidbody>();
        _Interact = GetComponent<HumanInteraction>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.started)
        {
            speed = speed * sprintMultiplyer;
        }
        if(controller.isGrounded && context.canceled)
        {
            speed = speed / sprintMultiplyer;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        _Interact?.TriggerInteract();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        if (!controller.isGrounded)
            velocity.y += gravity * Time.fixedDeltaTime;
        else if (velocity.y < 0)
            velocity.y = -2f;

        Vector3 finalMove = (movement * speed) + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.fixedDeltaTime);


    }
}
