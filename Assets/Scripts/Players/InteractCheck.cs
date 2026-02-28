using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public event Action<IInteractable> OnInteractEnter;
    public event Action<IInteractable> OnInteractExit;

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            OnInteractEnter?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            OnInteractExit?.Invoke(interactable);
        }
    }
}

