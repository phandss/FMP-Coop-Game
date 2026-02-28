using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HumanController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float sprintMultiplyer = 2;

    [SerializeField] private CharacterController controller;
    private HumanInteraction _interact;
    private Vector2 _moveInput;
    private Vector3 _velocity;
    private bool _isSprinting;

    private void Awake()
    {
        controller = GetComponentInChildren<CharacterController>();
        _interact = GetComponent<HumanInteraction>();

    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.performed && controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        { 
            _isSprinting = true;
        }

        if(context.canceled)
        { 
            _isSprinting = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        Debug.Log("interact key pressed");
        _interact?.TryInteract();
    }

    private void FixedUpdate()
    {
        

        float currentSpeed = _isSprinting ? speed * sprintMultiplyer : speed;
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        if (!controller.isGrounded)
        {
            _velocity.y += gravity * Time.deltaTime;
        }

        else if (_velocity.y < 0)
        {
            _velocity.y = -2f;
        }


        Vector3 finalMove = (movement * currentSpeed) + new Vector3(0, _velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);


    }
}
