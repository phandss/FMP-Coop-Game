using System.Collections;
using UnityEngine;

public class InteractDoor : InteractObjectBase
{
    [SerializeField] private Transform leftDoorPanel;
    [SerializeField] private Transform rightDoorPanel;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openTime = 0.8f;
    [SerializeField] private bool _isSwitchControlled = false;

    public override bool isInteractable => !_isAnimating && !_isLocked && !_isSwitchControlled;

    private bool _isAnimating;
    private bool _isOpen;
    private bool _isLocked;
    
    private Quaternion _leftClosedRot;
    private Quaternion _rightClosedRot;
    private Quaternion _leftOpenRot;
    private Quaternion _rightOpenRot;

    protected override void Awake()
    {
        base.Awake();

        _leftClosedRot = leftDoorPanel.localRotation;
        _rightClosedRot = rightDoorPanel.localRotation;

        _leftOpenRot = Quaternion.Euler(0f, -openAngle, 0f) * _leftClosedRot;
        _rightOpenRot = Quaternion.Euler(0f, openAngle, 0f) * _rightClosedRot;
    }

    public override void OnInteract()
    {
        if (!_isAnimating && !_isLocked && !_isSwitchControlled)
        {
            StartCoroutine(AnimateDoor(true));
        }
    }

    private IEnumerator AnimateDoor(bool open, bool lockDoor = false)
    {
        _isAnimating = true;

        Quaternion leftTarget = open ? _leftOpenRot : _leftClosedRot;
        Quaternion rightTarget = open ? _rightOpenRot : _rightClosedRot;

        Quaternion leftStart = leftDoorPanel.localRotation;
        Quaternion rightStart = rightDoorPanel.localRotation;

        float elapsed = 0f;

        while (elapsed < openTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openTime);
            float eased = Mathf.SmoothStep(0f, 1f, t);

            leftDoorPanel.localRotation = Quaternion.Lerp(leftStart, leftTarget, eased);
            rightDoorPanel.localRotation = Quaternion.Lerp(rightStart, rightTarget, eased);

            yield return null;
        }

        leftDoorPanel.localRotation = leftTarget;
        rightDoorPanel.localRotation = rightTarget;

        _isOpen = open;
        _isLocked = lockDoor;
        _isAnimating = false;
    }

    public void OnLockTrigger()
    {
        if (_isOpen && !_isAnimating)
        {
            StartCoroutine(AnimateDoor(false, true));
        }
    }

    public override void OnHoverEnter(string buttonPrompt)
    {
        if (!_isLocked && !_isAnimating)
        {
            base.OnHoverEnter(buttonPrompt);
        }

        if(_isSwitchControlled)
        {
            base.OnHoverEnter("Locked");
        }
    }

    public void SwitchOpen()
    {
        if (!_isAnimating && !_isLocked)
        {
            StartCoroutine(AnimateDoor(true));
        }
    }


}