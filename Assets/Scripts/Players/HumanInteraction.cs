using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanInteraction : MonoBehaviour
{
    [SerializeField] private Transform carryPoint;

    public float interactRange = 2f;
    public float holdThreshold = 0.3f;
    public LayerMask interactLayer;
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
        if(_pressedInteractable == null)
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
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
        IInteractable closest = null;
        float minDist = float.MaxValue;

        foreach (Collider col in hits)
        {
            IInteractable checkObj = col.GetComponentInParent<IInteractable>();
            if(checkObj == null) continue;

            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                closest = checkObj;
            }
        }
        return closest;
    }



    private void OnDrawGizmosSelected()
    { 
        Gizmos.color = new Color(0.2f, 0.8f, 0.2f, 0.25f);
        Gizmos.DrawSphere(transform.position, interactRange);
    }
}
