using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class HumanInteraction : MonoBehaviour
{

    private readonly List<IInteractable> _interactablesInRange = new List<IInteractable>();

    [SerializeField] private Transform carryPoint;
    [SerializeField] private InteractCheck iCheck;
    public float holdThreshold = 0.3f;


    public InputActionReference actionReference;

    private IInteractable _closestInteract;
    private IInteractable _pressedInteractable;
    private string _buttonPrompt;
    private float _pressTime;
    private bool _isCarrying;





    private void Awake()
    {
        _buttonPrompt = "E";
        if(actionReference != null)
        {
            string display = actionReference.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
            _buttonPrompt = string.IsNullOrEmpty(display) ? "E" : display;
        }

        iCheck.OnInteractEnter += HandleEntered;
        iCheck.OnInteractExit += HandleExited;

    }

    private void OnDestroy()
    {
        iCheck.OnInteractEnter -= HandleEntered;
        iCheck.OnInteractExit -= HandleExited;
    }

    private void HandleEntered(IInteractable interactable)
    {
        if(!_interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Add(interactable);

        }
    }

    private void HandleExited(IInteractable interactable)
    {
        _interactablesInRange.Remove(interactable);

        if (_closestInteract == interactable)
        {
            _closestInteract.OnHoverExit();
            _closestInteract = null;
        }
    }

    private void Update()
    {
        CheckClosestInteract();

        if(_pressedInteractable != null)
        {
            UpdateHoldCheck();
        }
    }

    private void UpdateHoldCheck()
    {
        bool confirmedHold = (Time.time - _pressTime) >= holdThreshold;

        if(!_isCarrying && confirmedHold && _pressedInteractable.isDraggable)
        {
            _isCarrying = true;
            _pressedInteractable.OnDragStart(carryPoint.position);
        }

        if(_isCarrying)
        {
            _pressedInteractable.OnDrag(carryPoint.position);
        }
    }

    public void OnInteractStart()
    {
        if (_closestInteract == null)
        {
            return;
        }
        _pressedInteractable = _closestInteract;
        _pressTime = Time.time;
        _isCarrying = false;
    }

    public void OnInteractEnd()
    {
        Debug.Log($"[HumanInteraction] Released — isCarrying: {_isCarrying}, pressed: {_pressedInteractable != null}");
        if (_pressedInteractable == null)
        {
            return;
        }
        if (_isCarrying)
        {
            _pressedInteractable.OnDragEnd();
        }
        else if (_pressedInteractable.isInteractable)
        {
            _pressedInteractable.OnInteract();
        }

        _pressedInteractable = null;
        _isCarrying = false;
    }



    private void CheckClosestInteract()
    {
        IInteractable closest = FindClosest();

        if(closest == _closestInteract)
        {
            return;
        }
        _closestInteract?.OnHoverExit();

        _closestInteract = closest;

        _closestInteract?.OnHoverEnter(_buttonPrompt);
    }

    private IInteractable FindClosest()
    {
        IInteractable closest = null;
        float minDist = float.MaxValue;

        foreach(IInteractable inter in _interactablesInRange)
        {
            MonoBehaviour mb = inter as MonoBehaviour;

            if(mb == null)
            {
                continue;
            }

            float dist = Vector3.Distance(transform.position, mb.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = inter;
            }
        }
        return closest;
    }



    private void OnDrawGizmosSelected()
    { 
        Gizmos.color = new Color(0.2f, 0.8f, 0.2f, 0.25f);
        Gizmos.DrawSphere(transform.position, 2f);
    }
}
