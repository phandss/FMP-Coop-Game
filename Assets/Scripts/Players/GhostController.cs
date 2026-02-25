using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    public Camera mainCam;
    public float rayRange = 1000000f;
    public LayerMask interactLayer;

    public float holdThreshold = 0.2f;

    private InputAction _interactAction;

    private IInteractable _hoveredInteractable;
    private IInteractable _pressedInteractable;
    private bool _isDragging;
    private float _pressTime;
    private Plane _dragPlane;
    private Vector3 _dragOffset;

    private void Awake()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    private void Update()
    {
        HoverUpdate();

        if(_pressedInteractable != null)
        {
            UpdateDragCheck();
        }
    }

    private void HoverUpdate()
    {
        if (_isDragging) return;

        Ray ray = GetRaycast();
        IInteractable hit = null;

        if(Physics.Raycast(ray, out RaycastHit hitInfo, rayRange, interactLayer))
        {
            hit = hitInfo.collider.GetComponentInParent<IInteractable>();
        }

        if(hit == _hoveredInteractable)
        {
            return;
        }

        _hoveredInteractable?.OnHoverExit();

        if (_hoveredInteractable != null)
        {
            if(_interactAction != null)
            {
                _hoveredInteractable.OnHoverEnter(_interactAction);
            }
            else
            {
                _hoveredInteractable.OnHoverEnter();
            }
        }
    }

    private void UpdateDragCheck()
    {
        bool heldLongEnough = (Time.time - _pressTime) >= holdThreshold;

        if (!_isDragging && heldLongEnough && _pressedInteractable.isDraggable)
        {

            _isDragging = true;
            _pressedInteractable.OnDragStart(GetMouseWorldOnPlane());
        }

        if (_isDragging)
        {
            Vector3 worldPos = GetMouseWorldOnPlane();
            _pressedInteractable.OnDrag(worldPos + _dragOffset);
        }
    }


    public void Click(InputAction.CallbackContext context) 
    {

        if(_interactAction == null)
        {
            _interactAction = context.action;
        }
        if (context.started) HandleMouseDown();
        if (context.canceled) HandleMouseUp();
    }

    private void HandleMouseUp()
    {
        if (_pressedInteractable == null)
            return;

        if (_isDragging)
        {

            _pressedInteractable.OnDragEnd();
        }
        else
        {

            if (_pressedInteractable.isInteractable)
                _pressedInteractable.OnInteract();
        }


        _pressedInteractable = null;
        _isDragging = false;
    }



    private void HandleMouseDown()
    {
        Ray ray = GetRaycast();
        

        if (!Physics.Raycast(ray, out RaycastHit hit, rayRange, interactLayer))
            return;

        IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
        if (interactable == null)
            return;

        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2f);

        _pressedInteractable = interactable;
        _pressTime = Time.time;
        _isDragging = false;

        if (interactable.isDraggable)
        {
            Vector3 objectPos = hit.collider.transform.position;
            _dragPlane = new Plane(Vector3.up, objectPos);
            _dragOffset = objectPos - hit.point;
        }
    }





    private Ray GetRaycast()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        return mainCam.ScreenPointToRay(mousePos);
    }

    private Vector3 GetMouseWorldOnPlane()
    {
        Ray ray = GetRaycast();
        if (_dragPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);


        Plane ground = new Plane(Vector3.up, Vector3.zero);
        ground.Raycast(ray, out float d);
        return ray.GetPoint(d);
    }
}
