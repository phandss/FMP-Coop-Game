using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public LayerMask interactLayer;

    public InputActionReference actionReference;

    private IInteractable _closestInteract;
    private string _buttonPrompt;




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
    }

    public void TryInteract()
    {
        if (_closestInteract == null)
        {
            return;
        }
        if (!_closestInteract.isInteractable)
        {
            return;
        }
        _closestInteract.OnInteract();
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
