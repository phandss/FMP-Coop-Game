using UnityEngine;

public class TestBox : InteractObjectBase
{
    [SerializeField] private float followSpeed = 12f;
    [SerializeField] private float carryHeightOffset = 0.5f;
    [SerializeField] private float carryForwardOffset = 1f;
    [SerializeField] private float throwMultiplier = 1.5f;

    public override bool isInteractable => true;
    public override bool isDraggable => true;

    private bool _isHeld;
    private Transform _humanTransform;
    private CharacterController _humanCC;
    private Vector3 _ghostTargetPos;
    private Vector3 _ghostDragVelocity;
    private Vector3 _lastGhostPos;
    private bool _heldByGhost;

    protected override void Awake()
    {
        base.Awake();

        HumanController human = FindFirstObjectByType<HumanController>();
        if (human != null)
        {
            _humanTransform = human.transform;
            _humanCC = human.GetComponent<CharacterController>();
        }
    }

    public override void OnInteract()
    {
        if (_isHeld)
        {
            Drop();
            return;
        }

        _isHeld = true;
        _heldByGhost = false;
        rb.useGravity = false;
        Debug.Log("Box picked up by Human");
    }

    public override void OnDragStart(Vector3 hitPoint)
    {
        if (_isHeld) return;

        _isHeld = true;
        _heldByGhost = true;
        rb.useGravity = false;
        _ghostTargetPos = transform.position;
        _lastGhostPos = transform.position;
        _ghostDragVelocity = Vector3.zero;
        Debug.Log("Box picked up by Ghost");
    }

    public override void OnDrag(Vector3 worldPos)
    {
        if (!_heldByGhost) return;

        _ghostDragVelocity = (worldPos - _lastGhostPos) / Time.deltaTime;
        _lastGhostPos = worldPos;
        _ghostTargetPos = worldPos;
    }

    public override void OnDragEnd()
    {
        if (!_heldByGhost) return;

        Drop();
        rb.linearVelocity = _ghostDragVelocity * throwMultiplier;
        Debug.Log("Box thrown by Ghost");
    }

    private void Drop()
    {
        if (_heldByGhost == false && _humanCC != null)
            rb.linearVelocity = _humanCC.velocity;

        _isHeld = false;
        _heldByGhost = false;
        rb.useGravity = true;
        Debug.Log("Box dropped");
    }

    private void FixedUpdate()
    {
        if (!_isHeld) return;

        Vector3 target = _heldByGhost ? _ghostTargetPos : GetHumanCarryPosition();
        rb.linearVelocity = (target - transform.position) * followSpeed;

        if (_heldByGhost)
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetHumanCarryPosition()
    {
        if (_humanTransform == null) return transform.position;

        return _humanTransform.position
            + _humanTransform.forward * carryForwardOffset
            + Vector3.up * carryHeightOffset;
    }
}