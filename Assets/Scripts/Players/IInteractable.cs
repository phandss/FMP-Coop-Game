using UnityEngine;

public interface IInteractable
{
    bool isClickable { get; }
    bool isDraggable { get; }


    void OnClick();

    void OnDragStart(Vector3 hitpoint);

    void OnDrag(Vector3 worldPos);

    void OnDragEnd();

    void OnHoverEnter();

    void OnHoverExit();
}
