using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HumanController : MonoBehaviour
{
    private HumanInteraction _interact;
    private HumanMovement _movement;
    private HumanHealth _health;

    private void Awake()
    {
        _interact = GetComponent<HumanInteraction>();
        _health = GetComponent<HumanHealth>();
        _movement = GetComponent<HumanMovement>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movement?.SetMoveInput(context.ReadValue<Vector2>());
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.performed)
        {
            _movement?.TryJump();
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        { 
            _movement?.SetSprinting(true);
        }

        if(context.canceled)
        { 
            _movement?.SetSprinting(false);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _interact?.OnInteractStart();
        }
        if(context.canceled)
        {
            _interact?.OnInteractEnd();
        }
    }


}
