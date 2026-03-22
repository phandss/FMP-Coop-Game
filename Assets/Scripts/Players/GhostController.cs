using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{

    [SerializeField]private Camera _mainCam;
    [SerializeField]private LayerMask _interactLayer;
    [SerializeField]private float _holdThreshold = 0.2f;
    [SerializeField]private InputActionReference _actionReference;
    [SerializeField] private float _scrollSpeed = 10f;
    [SerializeField] private float _minCarryHeight = .2f;
    [SerializeField] private float _maxCarryHeight = 10f;

    private string _buttonPrompt = "LMB";

    private IInteractable _hoveredInteractable;
    private IInteractable _pressedInteractable;
    private IMoveable _pressedMoveable;
    private bool _isDragging;
    private float _pressTime;
    private Plane _dragPlane;
    private Vector2 _dragOffset;
    private float _currentCarryHeight = 2f;



    private void Awake()
    {
        _mainCam = GetComponentInChildren<Camera>();
        if(_mainCam == null)
        {
            _mainCam = Camera.main;
        }

        if (_actionReference != null)
        {
            string display = _actionReference.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
            _buttonPrompt = string.IsNullOrEmpty(display) ? "LMB" : display;
        }
        
    }

    private void Update()
    {
        if (!_isDragging)
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

        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _interactLayer))
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
        bool confirmedHold = (Time.time - _pressTime) >= _holdThreshold;

        if (!_isDragging && confirmedHold && _pressedMoveable != null)
        {

            _isDragging = true;
            _pressedMoveable.OnDragStart(GetMouseWorldOnPlane());
            Debug.Log("Ghost started dragging " + _pressedInteractable);
        }

        if (_isDragging)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;

            if (scroll != 0f)
            {
                _currentCarryHeight = Mathf.Clamp(_currentCarryHeight - scroll * _scrollSpeed * Time.deltaTime, _minCarryHeight, _maxCarryHeight);
                _dragPlane = new Plane(Vector3.up, new Vector3(0f, _currentCarryHeight, 0f));
            }
            _pressedMoveable.OnDrag(UpdateDragHeight());
        }
    }

    private Vector3 UpdateDragHeight()
    {
        Vector3 mouseWorld = GetMouseWorldOnPlane();
        return new Vector3(mouseWorld.x + _dragOffset.x, _currentCarryHeight, mouseWorld.z + _dragOffset.y);
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
            _pressedMoveable.OnDragEnd();
        }

        else if (_pressedInteractable.isInteractable)
        { 
            _pressedInteractable.OnInteract();
        }

        _pressedInteractable = null;
        _pressedMoveable = null;
        _isDragging = false;
    }



    private void HandleMouseDown()
    {
        Ray ray = GetRaycast();
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _interactLayer, QueryTriggerInteraction.Collide))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
            return;
        }

        Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);

        Fracture fracture = hit.collider.GetComponent<Fracture>();
        if (fracture != null)
        {
            Rigidbody rb = fracture.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            fracture.CauseFracture();
            return;
        }

        IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
        if (interactable == null) 
        { 
            return; 
        }

        _pressedInteractable = interactable;

        _pressedMoveable = hit.collider.GetComponentInParent<IMoveable>();
        _pressTime = Time.time;
        _isDragging = false;


        if (_pressedMoveable != null)
        {
            Vector3 objectPos = hit.collider.transform.position;
            _currentCarryHeight = objectPos.y;
            _dragOffset = new Vector2(objectPos.x - hit.point.x, objectPos.z - hit.point.z);
            _dragPlane = new Plane(Vector3.up, new Vector3(0f, _currentCarryHeight, 0f));
        }
    }






    private Ray GetRaycast()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        return _mainCam.ScreenPointToRay(mousePos);
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
