using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    bool isInteractable { get; }
    bool isDraggable { get; }


    void OnInteract();

    void OnDragStart(Vector3 hitpoint);

    void OnDrag(Vector3 worldPos);

    void OnDragEnd();


    void OnHoverEnter(string buttonPrompt);

    void OnHoverExit();
}
