using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public LayerMask interactLayer;

    public InputActionReference actionReference;

    private IInteractable _closestInteract;
    private InputAction _interactAction;




    private void Awake()
    {
        if (actionReference != null)
        {
            _interactAction = actionReference.action;
        }
    }

    private void Update()
    {
        CheckClosestInteract();
    }

    private void CheckClosestInteract()
    {
        IInteractable closest = FindClosest();

        if(closest == _closestInteract)
        {
            return;
        }

        _closestInteract = closest;

        if (_closestInteract != null)
        {
            if(_interactAction != null)
            {
                _closestInteract.OnHoverEnter(_interactAction);
            }
            else
            {
                _closestInteract.OnHoverEnter();
            }
        }
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
