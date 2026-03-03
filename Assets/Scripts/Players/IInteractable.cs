using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    bool isInteractable { get; }



    void OnInteract();

    void OnHoverEnter(string buttonPrompt);

    void OnHoverExit();
}

public interface IMoveable
{
    void OnDragStart(Vector3 hitpoint);
    void OnDrag(Vector3 worldPos);
    void OnDragEnd();
}
