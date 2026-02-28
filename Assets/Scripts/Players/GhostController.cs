using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{

    [SerializeField]private Camera mainCam;
    [SerializeField]private LayerMask interactLayer;
    [SerializeField]private float holdThreshold = 0.2f;
    [SerializeField]private InputActionReference actionReference;

    private string _buttonPrompt = "LMB";

    private IInteractable _hoveredInteractable;
    private IInteractable _pressedInteractable;
    private bool _isDragging;
    private float _pressTime;
    private Plane _dragPlane;
    private Vector3 _dragOffset;

    private void Awake()
    {
        mainCam = GetComponentInChildren<Camera>();
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }

        if (actionReference != null)
        {
            string display = actionReference.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
            _buttonPrompt = string.IsNullOrEmpty(display) ? "LMB" : display;
        }
        
    }

    private void Update()
    {
        if (_isDragging)
        {
            HoverUpdate();
        }

        if (_pressedInteractable != null) 
        { 
            UpdateDragCheck();
        }
    }

    private void HoverUpdate()
    {


        Ray ray = GetRaycast();
        IInteractable hit = null;

        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, interactLayer))
        {
            hit = hitInfo.collider.GetComponentInParent<IInteractable>();
        }

        if(hit == _hoveredInteractable)
        {
            return;
        }

        _hoveredInteractable?.OnHoverExit();
        _hoveredInteractable = hit;
        _hoveredInteractable?.OnHoverEnter(_buttonPrompt);
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
            _pressedInteractable.OnDrag(GetMouseWorldOnPlane() + _dragOffset);
        }
    }


    public void Click(InputAction.CallbackContext context) 
    {

        if (context.started)
        { 
            HandleMouseDown(); 


        }
        if (context.canceled)
        { 
            HandleMouseUp();

        }

    }

    private void HandleMouseUp()
    {
        if (_pressedInteractable == null)
        {
            return;
        }

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

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
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
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
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
